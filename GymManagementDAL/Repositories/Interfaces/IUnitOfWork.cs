using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        ISessionRepository SessionRepository { get; set; }
        public IBookingRepository BookingRepository { get; }
        public IMembershipRepository MembershipRepository { get; }
        int SaveChanges();
    }
}
