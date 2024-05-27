using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Web.Controllers;

public class StudentsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}