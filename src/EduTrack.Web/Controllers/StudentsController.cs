using EduTrack.Service.Services.Students;
using EduTrack.Web.Models.Students;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Web.Controllers;

public class StudentsController (IStudentService studentService) : Controller
{
    public async ValueTask<IActionResult> Index(int ? page, string search = null)
    {
        return View(new StudentListModel
        {
            Students = await studentService.GetAllAsync(page, search)
        }); ;
    }
}