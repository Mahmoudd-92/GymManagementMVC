using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementDAL.Entities
{
    [Table("Members")]
    public class HealthRecord: BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }
    }
}
