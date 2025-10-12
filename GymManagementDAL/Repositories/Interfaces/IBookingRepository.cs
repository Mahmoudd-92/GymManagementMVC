using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking? GetById(int id);
        int Add(Booking booking);
        int Update(Booking booking);
        int Delete(int id);
    }
}
