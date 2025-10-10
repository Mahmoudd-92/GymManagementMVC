using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Ignore(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Plan)
                .WithMany(x => x.PlanMembers)
                .HasForeignKey(x => x.PlanId);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.MemberPlans)
                .HasForeignKey(x => x.MemberId);

            builder.HasKey(x => new { x.MemberId, x.PlanId });
        }
    }
}
