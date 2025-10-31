using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser (LoginViewModel input );
    }
}
