using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        Trainer? GetById(int id);
        IEnumerable<Trainer> GetAll();
        int Add(Trainer trainer);
        int Update(Trainer trainer);
        int Delete(int id);
    }
}
