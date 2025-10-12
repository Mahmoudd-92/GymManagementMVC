using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IHealthRecordRepository
    {
        IEnumerable<HealthRecord> GetAll();
        HealthRecord? GetById(int id);
        int Add(HealthRecord healthRecord);
        int Update(HealthRecord healthRecord);
        int Delete(int id);
    }
}
