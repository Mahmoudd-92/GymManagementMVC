using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    public class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x=> x.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.HealthRecord)
                .WithOne()
                .HasForeignKey<HealthRecord>(x => x.Id);

            base.Configure(builder);
        }
    }
}
