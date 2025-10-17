using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Activate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

            if (plan is null || HasActiveMemberships(PlanId))
                return false;

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.GetRepository<Plan>().Update(plan);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();

            if (plans is null || !plans.Any())
                return [];

            return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive
            });
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

            if (plan is null)
                return null;

            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

            if (plan is null || plan.IsActive == false)
                return null;

            return new UpdatePlanViewModel
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price
            };
        }
        
        public bool UpdatePlan(int id, UpdatePlanViewModel input)
        {
            try
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetById(id);

                if (plan is null || HasActiveMemberships(id))
                    return false;

                plan.Name = input.PlanName;
                plan.Description = input.Description;
                plan.DurationDays = input.DurationDays;
                plan.Price = input.Price;

                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Helper Methods

        private bool HasActiveMemberships(int PlanId)
        {
            return _unitOfWork.GetRepository<Membership>()
                    .GetAll(m => m.PlanId == PlanId && m.Status == "Active")
                    .Any();
        }

        #endregion
    }
}