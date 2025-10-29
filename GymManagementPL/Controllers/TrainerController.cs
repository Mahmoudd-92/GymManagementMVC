using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }
        
        public IActionResult Index()
        {
            var trainers = trainerService.GetAllTrainers();
            return View(trainers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data!");
                return View(nameof(Create), input);
            }
            bool result = trainerService.CreateTrainer(input);

            if (result)
                TempData["SuccessMessage"] = "Trainer created Successfully!";
            else
                TempData["ErrorMessage"] = "Trainer failed to create, Phone Number or E-mail already exists.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var trainers = trainerService.GetTrainerDetails(id);

            if (trainers == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainers);
        }

        public IActionResult Edit(int id)
        {
            var trainers = trainerService.GetTrainerToUpdate(id);

            if (trainers is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainers);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdateTrainerViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            bool result = trainerService.UpdateTrainerDetails(id, input);

            if (result)
                TempData["SuccessMessage"] = "Trainer Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Update, Phone Number or Email Already Exist!";

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid ID";
                return RedirectToAction(nameof(Index));
            }
            var trainers = trainerService.GetTrainerDetails(id);

            if (trainers is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MemberId = id;
            return View();
        }

        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = trainerService.RemoveTrainer(id);

            if (result)
                TempData["SuccessMessage"] = "Trainer Deleted Successfully!";
            else
                TempData["ErrorMessage"] = "Trainer can not be deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}
