using GymManagementDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Entities
{
    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
    }
    public abstract class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }
}
