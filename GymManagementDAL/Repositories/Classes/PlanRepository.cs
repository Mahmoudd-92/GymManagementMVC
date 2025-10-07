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
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _context;

        public PlanRepository(GymDbContext context)
        {
            _context = context;
        }

        public int Add(Plan plan)
        {
            _context.Plans.Add(plan);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var plan = GetById(id);
            if (plan is null)
                return 0;

            _context.Remove(plan);
            return _context.SaveChanges();
        }

        public IEnumerable<Plan> GetAll() => _context.Plans.ToList();

        public Plan? GetById(int id) => _context.Plans.Find(id);

        public int Update(Plan plan)
        {
            _context.Update(plan);
            return _context.SaveChanges();
        }
    }
}
