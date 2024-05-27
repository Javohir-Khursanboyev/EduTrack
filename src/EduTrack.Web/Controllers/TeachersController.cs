using EduTrack.Service.Services.Students;
using EduTrack.Web.Models.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Web.Controllers;

public class TeachersController(ITeacherService teacherService) : Controller
{
    public async ValueTask<IActionResult> Index(int? page, string search = null)
    {
        return View(new TeacherListModel
        {
            Teachers = await teacherService.GetAllAsync(page, search)
        });
    }
}