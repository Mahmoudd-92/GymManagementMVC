using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementDAL.Data.DataSeed
{
    public static class IdentityDataSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = "SuperAdmin"},
                    new IdentityRole { Name = "admin" }
                };

                    foreach (var role in roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name).Result)
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }

                if (!userManager.Users.Any())
                {
                    var superAdmin = new ApplicationUser
                    {
                        FirstName = "Mahmoud",
                        LastName = "Mohamed",
                        UserName = "Mahmoud",
                        Email = "Mahmoud@gmail.com",
                        PhoneNumber = "01234567899"
                    };

                    userManager.CreateAsync(superAdmin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(superAdmin, "SuperAdmin").Wait();

                    var admin = new ApplicationUser
                    {
                        FirstName = "Omar",
                        LastName = "Ahmed",
                        UserName = "OmarAhmed",
                        Email = "Ahmed@gmail.com",
                        PhoneNumber = "01052345979"
                    };
                    userManager.CreateAsync(admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(admin, "admin").Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding failed duo to: {ex}");
                return false;
            }
        }
    }
}

