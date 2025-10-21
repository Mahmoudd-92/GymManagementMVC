using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateTrainer(CreateTrainerViewModel model)
        {
            if(IsEmailExist(model.Email))
                return false;

            if(IsPhoneExist(model.Phone)) 
                return false;

            var trainer = new Trainer
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Specialties = model.Specialities,
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                }
            };

            _unitOfWork.GetRepository<Trainer>().Add(trainer);
            _unitOfWork.SaveChanges();
            return true;

        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll().ToList() ?? [];

            if (trainers is null || !trainers.Any())
                return [];

            var trainerViewModel = trainers.Select(x => new TrainerViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialties = x.Specialties.ToString(),
            });

            return trainerViewModel;
        }

        public TrainerViewModel GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if (trainer is null)
                return null;

            var trainerViewModel = new TrainerViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender.ToString(),
                DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                HiringDate = trainer.CreatedAt.ToShortDateString(),
                Specialties = trainer.Specialties.ToString(),
                Address = FormatAddress(trainer.Address)
            };

            return trainerViewModel;
        }

        public UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if (trainer is null)
                return null;

            var trainerToUpdateViewModel = new UpdateTrainerViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNumber,
                City = trainer.Address.City,
                Street = trainer.Address.Street,
                Specialities = trainer.Specialties
            };

            return trainerToUpdateViewModel;
        }

        public bool RemoveTrainer(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if (trainer is null)
                return false;

            var upcomingSessions = _unitOfWork.GetRepository<Session>()
                                    .GetAll(x => x.TrainerId == trainerId && x.StartDate > DateTime.Now);

            if (upcomingSessions.Any())
                return false;

            _unitOfWork.GetRepository<Trainer>().Delete(trainer);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateTrainerDetails(int trainerId, UpdateTrainerViewModel model)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if(trainer is null)
                return false;

            if (IsEmailExist(model.Email))
                return false;

            if (IsPhoneExist(model.Phone))
                return false;

            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Gender = model.Gender;
            trainer.Specialties = model.Specialities;
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Address.City = model.City;
            trainer.Address.Street = model.Street;
            trainer.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Trainer>().Update(trainer);
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
            var existingTrainer = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email);
            return existingTrainer is not null && existingTrainer.Any();
        }

        private bool IsPhoneExist(string phone)
        {
            var existingTrainer = _unitOfWork.GetRepository<Trainer>().GetAll(y => y.Phone == phone);
            return existingTrainer is not null && existingTrainer.Any();
        }

        #endregion
    }
}
