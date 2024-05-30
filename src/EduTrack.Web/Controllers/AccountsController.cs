using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Web.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
