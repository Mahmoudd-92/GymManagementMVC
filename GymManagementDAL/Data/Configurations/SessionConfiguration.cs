using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    public class SessionConfiguration: IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasOne(x => x.Trainer)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.TrainerId);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.CategoryId);

            builder.ToTable(x =>
            {
                x.HasCheckConstraint("Session_CapacityCheck", "Capacity Between 1 and 25");
                x.HasCheckConstraint("Session_EndDateCheck", "EndDate > StartDate");
            });
        }
    }
}
