using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory(Func<Session, bool>? condition = null);
        Session GetSessionWithTrainerAndCategory(int sessionId);
        int GetCountOfBookedSlots(int sessionId);
    }
}