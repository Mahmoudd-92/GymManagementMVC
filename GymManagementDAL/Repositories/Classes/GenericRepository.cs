using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GymDbContext _context;

        public GenericRepository(GymDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity) => _context.Add(entity);

        public void Delete(TEntity entity) => _context.Remove(entity);

        public bool Exists(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if(condition is not null)
                return _context.Set<TEntity>().AsNoTracking().Where(condition).ToList();
            else
                return _context.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity? GetById(int id) => _context.Set<TEntity>().Find(id);
       

        public void Update(TEntity entity) => _context.Update(entity);
    }
}
