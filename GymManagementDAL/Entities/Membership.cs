namespace GymManagementDAL.Entities
{
    public class Membership : BaseEntity
    {
        public DateTime EndDate { get; set; }
        public string Status 
        {
            get 
            {
                if (EndDate >= DateTime.Now)
                    return "Active";
                else
                    return "Expired";
            } 
        }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
    }
}
