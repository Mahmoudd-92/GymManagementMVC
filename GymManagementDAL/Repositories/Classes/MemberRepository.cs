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
    public class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _context;

        public MemberRepository(GymDbContext context)
        {
            _context = context;
        }

        public int Add(Member member)
        {
            _context.Members.Add(member);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var member = GetById(id);
            if (member is null)
                return 0;

            _context.Remove(member);
            return _context.SaveChanges();
        }

        public IEnumerable<Member> GetAll() => _context.Members.ToList();

        public Member? GetById(int id) => _context.Members.Find(id);

        public int Update(Member member)
        {
            _context.Update(member);
            return _context.SaveChanges();
        }
    }
}
