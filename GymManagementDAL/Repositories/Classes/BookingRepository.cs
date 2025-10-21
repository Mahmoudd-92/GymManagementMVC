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
    public class BookingRepository : IBookingRepository
    {
        private readonly GymDbContext _context;

        public BookingRepository(GymDbContext context)
        {
            _context = context;
        }

        public int Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var booking = GetById(id);
            if (booking is null)
                return 0;

            _context.Remove(booking);
            return _context.SaveChanges();
        }

        public IEnumerable<Booking> GetAll() => _context.Bookings.ToList();

        public Booking? GetById(int id) => _context.Bookings.Find(id);

        public int Update(Booking booking)
        {
            _context.Update(booking);
            return _context.SaveChanges();
        }
    }
}
