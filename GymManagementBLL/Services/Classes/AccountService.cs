using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public ApplicationUser? ValidateUser(LoginViewModel input)
        {
            var user = userManager.FindByEmailAsync(input.Email).Result;

            var isValidPassword = userManager.CheckPasswordAsync(user, input.Password).Result;

            return isValidPassword ? user : null;
        }
    }
}
