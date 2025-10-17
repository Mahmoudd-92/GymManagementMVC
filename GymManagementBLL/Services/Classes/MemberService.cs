using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel model)
        {
            try
            {
                if(IsEmailExist(model.Email))
                    return false;
                if(IsPhoneExist(model.Phone)) 
                    return false;

                var member = new Member
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Address = new Address
                    {
                        BuildingNumber = model.BuildingNumber,
                        City = model.City,
                        Street = model.Street
                    },
                    HealthRecord = new HealthRecord
                    {
                        Height = model.HealthRecordViewModel.Height,
                        Weight = model.HealthRecordViewModel.Weight,
                        BloodType = model.HealthRecordViewModel.BloodType,
                        Note = model.HealthRecordViewModel.Note
                    }
                };

                _unitOfWork.GetRepository<Member>().Add(member);

                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll().ToList() ?? [];

            if (members is null || !members.Any())
                return [];

            var memberViewModel = members.Select(x => new MemberViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Photo = x.Photo,
                Email = x.Email,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth.ToShortDateString(),
                Gender = x.Gender.ToString()
            });

            return memberViewModel;
        }

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

            if (member is null) 
                return null;

            var memberViewModel = new MemberViewModel
            {
                Id= member.Id,
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = FormatAddress(member.Address),
            };

            var activeMembership = _unitOfWork.GetRepository<Membership>()
                                    .GetAll(x => x.MemberId == memberId && x.Status == "Active")
                                    .FirstOrDefault();

            if(activeMembership is not null)
            {
                var activePlan = _unitOfWork.GetRepository<Plan>().GetById(activeMembership.PlanId);

                memberViewModel.PlanName = activePlan?.Name;
                memberViewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
            }

            return memberViewModel;
        }

        public HealthRecordViewModel GetMemberHealthRecord(int memberId)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);

            if (memberHealthRecord is null)
                return null;

            var healthRecordViewModel = new HealthRecordViewModel
            {
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                BloodType = memberHealthRecord.BloodType,
                Note = memberHealthRecord.Note
            };

            return healthRecordViewModel;
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

            if (member is null)
                return null;

            var memberToUpdateViewModel = new MemberToUpdateViewModel
            {
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City, 
                Street = member.Address.Street
            };

            return memberToUpdateViewModel;
        }

        public bool RemoveMember(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

            if (member is null)
                return false;

            var activeBookings = _unitOfWork.GetRepository<Booking>()
                                 .GetAll(x => x.MemberId == memberId && x.Session.StartDate > DateTime.Now);

            if(activeBookings.Any())
                return false;

            var memberships = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == memberId).ToList();

            try
            {
                if (memberships.Any())
                {
                    memberships.ForEach(membership =>
                    {
                        _unitOfWork.GetRepository<Membership>().Delete(membership);
                    });
                }

                _unitOfWork.GetRepository<Member>().Delete(member);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateMemberDetails(int memberId, MemberToUpdateViewModel model)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

            if (member is null)
                return false;

            if(IsEmailExist(model.Email))
                return false;

            if(IsPhoneExist(model.Phone)) 
                return false;

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.City = model.City;   
            member.Address.Street = model.Street; 
            member.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Member>().Update(member);
            _unitOfWork.SaveChanges();

            return true;
        }

        #region Helper Methods

        private string FormatAddress(Address address)
        {
            if (address is null)
                return "N/A";

            return $"{address.BuildingNumber}, {address.Street}, {address.City}";
        }

        private bool IsEmailExist(string email)
        {
            var existingMember = _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email);
            return existingMember is not null && existingMember.Any();
        }

        private bool IsPhoneExist(string phone) {
            var existingMember = _unitOfWork.GetRepository<Member>().GetAll(y => y.Phone == phone);
            return existingMember is not null && existingMember.Any();
        }

        #endregion
    }
}
