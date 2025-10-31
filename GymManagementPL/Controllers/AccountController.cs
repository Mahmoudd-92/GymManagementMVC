using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
		{
			_accountService = accountService;
			_signInManager = signInManager;
		}

		public ActionResult Login()
		{
			if (User?.Identity?.IsAuthenticated ?? false)
				return RedirectToAction(nameof(HomeController.Index), "Home");
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var User = _accountService.ValidateUser(model);
			if (User is null)
			{
				ModelState.AddModelError("Invalid Login", "Invalid E-mail or Password.");
				return View(model);
			}

			var Result = _signInManager.PasswordSignInAsync(User,
				model.Password,
				model.RememberMe,
				false).Result;

			if (Result.IsNotAllowed)
				ModelState.AddModelError("Invalid Login", "Your Account Is Not Allowed");
			if (Result.IsLockedOut)
				ModelState.AddModelError("Invalid Login", "Your Account Is Locked Out ");
			if (Result.Succeeded)
				return RedirectToAction(nameof(HomeController.Index), "Home");

			ModelState.AddModelError("Invalid Login", "Login Failed");
			return View(model);
		}
		 

		[HttpPost]
		public ActionResult Logout()
		{
			_signInManager.SignOutAsync().GetAwaiter().GetResult();
			return RedirectToAction(nameof(Login));
		}
 

		public ActionResult AccessDenied()
		{
			return View();
		}
	}
}
