using Microsoft.AspNetCore.Mvc;
using Gistapp.Models;
using Gistapp.Services;
using DocumentFormat.OpenXml.Spreadsheet;
namespace Gistapp.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                _memberService.CreateMember(member);
                return RedirectToAction("Index");
            }
            return View(member);
        }
        public IActionResult Delete(int id)
        {
            var member = _memberService.GetMemberById(id);
            return member == null ? NotFound() : View(member);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _memberService.DeleteMember(id);
            return RedirectToAction("Index");
        }
    }
}
