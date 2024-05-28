using EduTrack.Service.DTOs.Teachers;
using EduTrack.Service.Exceptions;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TeacherCreateModel createModel)
    {
        if (!ModelState.IsValid)
        {
            return View(createModel);
        }

        try
        {
            await teacherService.CreateAsync(createModel);
            return RedirectToAction("Index");
        }
        catch (AlreadyExistException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            // Log the exception (ex) here as needed.
            return View();
        }
    }

    public async ValueTask<IActionResult> Update(long id)
    {
        try
        {
            var teacher = await teacherService.GetByIdAsync(id);
            var updateModel = new TeacherUpdateModel
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };

            return View(updateModel);
        }
        catch(NotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpPost]
    public async ValueTask<IActionResult> Update(long id, TeacherUpdateModel updateModel)
    {
        if (!ModelState.IsValid)
        {
            return View(updateModel);
        }

        try
        {
            await teacherService.UpdateAsync(id, updateModel);
            return RedirectToAction("Index");
        }
        catch (NotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
        catch (AlreadyExistException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            // Log the exception here as needed.
            return View();
        }
    }

    public async ValueTask<IActionResult> Delete(long id)
    {
        try
        {
            await teacherService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        catch (NotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }
}