using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _context;

        public SessionRepository(GymDbContext context)
        {
            _context = context;
        }

        public int Add(Session session)
        {
            _context.Sessions.Add(session);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var session = GetById(id);
            if (session is null)
                return 0;

            _context.Remove(session);
            return _context.SaveChanges();
        }

        public IEnumerable<Session> GetAll() => _context.Sessions.ToList();

        public Session? GetById(int id) => _context.Sessions.Find(id);

        public int Update(Session session)
        {
            _context.Update(session);
            return _context.SaveChanges();
        }
    }
}
