using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }
        public IActionResult Index()
        {
            var Plans = planService.GetAllPlans();
            return View(Plans);
        }

        public IActionResult Details(int id)
        {
            var plan = planService.GetPlanById(id);
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Plan ID can Not be Zero or Negative!";

                return RedirectToAction(nameof(Index)); 
            }

            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        public IActionResult Edit(int id)
        {
            var Plans = planService.GetPlanToUpdate(id);
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Plan ID can Not be Zero or Negative!";

                return RedirectToAction(nameof(Index));
            }
            if (Plans is null)
            {
                TempData["ErrorMessage"] = "Plan can not be updated.";
                return RedirectToAction(nameof(Index));
            }

            return View(Plans);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatePlanViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Validation");
                return View(input);
            }

            var result = planService.UpdatePlan(id, input);

            if (result)
                TempData["SuccessMessage"] = "Plan Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Plan Failed To Update.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Activate(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "Plan ID can Not be Zero or Negative!";

                return RedirectToAction(nameof(Index));
            }

            var activate = planService.Activate(Id);

            if (activate)
                TempData["SuccessMessage"] = "Plan Activated Successfully!";
            else
                TempData["ErrorMessage"] = "Plan Failed To Activate !";

            return RedirectToAction(nameof(Index));
        }
    }
}
