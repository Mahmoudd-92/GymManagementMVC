using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    public class GymUserConfiguration : IEntityTypeConfiguration<GymUser>
    {
        public void Configure(EntityTypeBuilder<GymUser> builder)
        {
            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(x => x.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(x => x.Phone)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.BuildingNumber)
                       .HasColumnName("BuildingNumber");

                address.Property(a => a.City)
                       .HasColumnType("varchar")
                       .HasColumnName("City")
                       .HasMaxLength(30);

                address.Property(a => a.Street)
                       .HasColumnType("varchar")
                       .HasColumnName("Street")
                       .HasMaxLength(30);
            });
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.ToTable(x =>
            {
                x.HasCheckConstraint("GymUser_EmailCheck", "Email LIKE '_%@_%._%'");
                x.HasCheckConstraint("GymUser_PhoneCheck", "(LEN(Phone) = 11 AND Phone NOT LIKE '%[^0-9]%' AND (Phone LIKE '010%' OR Phone LIKE '011%' OR Phone LIKE '012%' OR Phone LIKE '015%')");
            });
        }
    }
}
