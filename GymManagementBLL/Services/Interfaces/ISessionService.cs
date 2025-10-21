using GymManagementBLL.ViewModels;
using GymManagementSystemBLL.ViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int sessionId);
        bool CreateSession(CreateSessionViewModel input);
        bool UpdateSession(int sessionId, UpdateSessionViewModel input);
        bool RemoveSession(int sessionId);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
    }
}