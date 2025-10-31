using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = memberService.GetAllMembers();
            return View(members);
        }
        public IActionResult MemberDetails(int id)
        {
            var member = memberService.GetMemberDetails(id);

            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public IActionResult HealthRecordDetails(int id)
        {
            var member = memberService.GetMemberHealthRecord(id);

            if (member == null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data!");
                return View(nameof(Create), input);
            }
            bool result = memberService.CreateMember(input);

            if (result)
                TempData["SuccessMessage"] = "Member created Successfully!";
            else
                TempData["ErrorMessage"] = "Member failed to create, Phone Number or E-mail already exists.";

                return RedirectToAction(nameof(Index));
        }
        public IActionResult MemberEdit(int id)
        {
            var member = memberService.GetMemberToUpdate(id);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult MemberEdit([FromRoute] int id, MemberToUpdateViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            bool result = memberService.UpdateMemberDetails(id, input);

            if (result)
                TempData["SuccessMessage"] = "Member Updated Successfully!";
            else
                TempData["ErrorMessage"] = "Member Failed To Update, Phone Number or Email Already Exist!";

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete([FromRoute]int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid ID";
                return RedirectToAction(nameof(Index));
            }
            var Member = memberService.GetMemberDetails(id);

            if (Member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MemberId = id;
            return View();
        }

        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = memberService.RemoveMember(id);

            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully!";
            else
                TempData["ErrorMessage"] = "Member can not be deleted.";

            return RedirectToAction(nameof(Index));
        }

    }
}
