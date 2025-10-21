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
    public class MembershipRepository : IMembershipRepository
    {
        private readonly GymDbContext _context;

        public MembershipRepository(GymDbContext context)
        {
            _context = context;
        }

        public int Add(Membership membership)
        {
            _context.Memberships.Add(membership);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var membership = GetById(id);
            if (membership is null)
                return 0;

            _context.Remove(membership);
            return _context.SaveChanges();
        }

        public IEnumerable<Membership> GetAll() => _context.Memberships.ToList();

        public Membership? GetById(int id) => _context.Memberships.Find(id);

        public int Update(Membership membership)
        {
            _context.Update(membership);
            return _context.SaveChanges();
        }
    }
}
