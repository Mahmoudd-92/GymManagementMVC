using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMembershipRepository
    {
        IEnumerable<Membership> GetAll();
        Membership? GetById(int id);
        int Add(Membership membership);
        int Update(Membership membership);
        int Delete(int id);
    }
}
