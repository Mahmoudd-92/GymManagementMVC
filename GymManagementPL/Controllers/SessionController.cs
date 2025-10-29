using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var sessions = sessionService.GetAllSessions();
            return View(sessions);
        }

        public IActionResult Create()
        {
            LoadCategoriesDropDown();
            LoadTrainersDropDown();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSessionViewModel input)
        {
            if (!ModelState.IsValid)
            {
                LoadCategoriesDropDown();
                LoadTrainersDropDown();
                return View(input);
            }
            var result = sessionService.CreateSession(input);

            if (result)
            {
                TempData["SuccessMessage"] = "Session created Successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Failed to create session. Please verify Trainer and category exist.");
                LoadCategoriesDropDown();
                LoadTrainersDropDown();
                return View(input);
            }
        }
        public IActionResult Details(int id)
        {
            var session = sessionService.GetSessionById(id);
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Session ID can Not be Zero or Negative!";

                return RedirectToAction(nameof(Index));
            }

            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        } 

        public IActionResult Edit(int id)
        {
            var session = sessionService.GetSessionToUpdate(id);

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Session ID Can not be Zero or Negative!";
                return RedirectToAction(nameof(Index));
            }

            if (session is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            LoadTrainersDropDown();
            return View(session);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdateSessionViewModel input)
        {
            if (!ModelState.IsValid)
            {
                LoadTrainersDropDown();
                return View(input);
            }

            var result = sessionService.UpdateSession(id, input);

            if (result)
                TempData["SuccessMessage"] = "Session Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Session Failed To Update.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var session = sessionService.GetSessionById(id);

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID cannot be Null or Negative!";
                return RedirectToAction(nameof(Index));
            }
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View(session);
        }
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = sessionService.RemoveSession(id);

            if (result)
                TempData["SuccessMessage"] = "Session Deleted Successfully!";
            else
                TempData["ErrorMessage"] = "Session can not be deleted.";

            return RedirectToAction(nameof(Index));
        }

        #region Helper Methods
        private void LoadCategoriesDropDown()
        {
            var categories = sessionService.GetCategoriesDropDown();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        private void LoadTrainersDropDown()
        {
            var trainers = sessionService.GetTrainersDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        #endregion
    }
}
