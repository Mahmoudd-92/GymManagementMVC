using GymManagementBLL.ViewModels.TrainerViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        bool CreateTrainer(CreateTrainerViewModel trainer);
        bool RemoveTrainer(int trainerId);
        bool UpdateTrainerDetails(int trainerId, UpdateTrainerViewModel model);
        IEnumerable<TrainerViewModel> GetAllTrainers();
        TrainerViewModel? GetTrainerDetails(int trainerId);
        UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId); 
    }
}
