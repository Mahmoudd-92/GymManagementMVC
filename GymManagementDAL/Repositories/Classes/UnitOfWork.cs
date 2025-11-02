using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _context;

        private readonly Dictionary<string, object> repositories = [];

        public IMembershipRepository MembershipRepository { get; }
        public IBookingRepository BookingRepository { get; }

        public ISessionRepository SessionRepository { get; set; }
        public UnitOfWork(GymDbContext context, 
            ISessionRepository sessionRepository, 
            IMembershipRepository membershipRepository, 
            IBookingRepository bookingRepository)
        {
            _context = context;
            SessionRepository = sessionRepository;
            MembershipRepository = membershipRepository;
            BookingRepository = bookingRepository;
        }


        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var entityName = typeof(TEntity).Name;

            if (repositories.TryGetValue(entityName, out object? value))
                return (IGenericRepository<TEntity>)value;

            var repository = new GenericRepository<TEntity>(_context);
            repositories.Add(entityName, repository);
            return repository;
        }

        public int SaveChanges() => _context.SaveChanges();
    }
}
