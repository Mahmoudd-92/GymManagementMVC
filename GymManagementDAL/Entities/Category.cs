namespace GymManagementDAL.Entities
{
    public class Category: BaseEntity
    {
        public string CategoryName { get; set; } = null!;
        public ICollection<Session> Sessions { get; set; } = null!;

    }
}
