namespace GymManagementDAL.Entities
{
    public class Member : GymUser
    {
        public string Photo { get; set; } = null!;
        public HealthRecord HealthRecord { get; set; } = null!;
        public ICollection<Membership> MemberPlans { get; set; } = null!;
        public ICollection<Booking> MemberSessions { get; set; } = null!;
    }
}
