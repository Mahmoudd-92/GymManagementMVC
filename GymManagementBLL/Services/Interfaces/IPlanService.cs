using GymManagementBLL.ViewModels.PlanViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IPlanService
    {
        bool UpdatePlan(int id, UpdatePlanViewModel input);
        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int PlanId);
        bool Activate(int PlanId);
    }
}