using AutoMapper;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Domain.Entities;
using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Attendences;
using EduTrack.Service.Exceptions;
using EduTrack.Service.Extensions;
using X.PagedList;

namespace EduTrack.Service.Services.Attendences;

public class AttendenceService
    (IMapper mapper, 
    IUnitOfWork unitOfWork) : IAttendenceService
{
    public async ValueTask<AttendenceViewModel> CreateAsync(AttendenceCreateModel createModel)
    {
        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == createModel.LessonId)
            ?? throw new NotFoundException($"Lesson is not found");

        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == createModel.StudentId)
            ?? throw new NotFoundException($"Student is not found");

        var mappedAttendence = mapper.Map<Attendence>(createModel);
        mappedAttendence.Create();

        var createdAttendence = await unitOfWork.Attendences.InsertAsync(mappedAttendence);
        await unitOfWork.SaveAsync();

        return mapper.Map<AttendenceViewModel>(createdAttendence);
    }

    public async ValueTask<AttendenceViewModel> UpdateAsync(long id, AttendenceUpdateModel updateModel)
    {
        var existAttendence = await unitOfWork.Attendences.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException($"Attendence is not found");

        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == updateModel.LessonId)
            ?? throw new NotFoundException($"Lesson is not found");

        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == updateModel.StudentId)
            ?? throw new NotFoundException($"Student is not found");

        mapper.Map(updateModel, existAttendence);
        existAttendence.Update();

        var updatedAttendence = await unitOfWork.Attendences.UpdateAsync(existAttendence);
        await unitOfWork.SaveAsync();

        return mapper.Map<AttendenceViewModel>(updatedAttendence);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existAttendence = await unitOfWork.Attendences.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException($"Attendence is not found");

        existAttendence.Delete();
        await unitOfWork.Attendences.DeleteAsync(existAttendence);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<AttendenceViewModel> GetByIdAsync(long id)
    {
        var existAttendence = await unitOfWork.Attendences.SelectAsync(a => a.Id == id, includes: ["Lesson", "Student"])
            ?? throw new NotFoundException($"Attendence is not found");

        return mapper.Map<AttendenceViewModel>(existAttendence);
    }

    public async ValueTask<IPagedList<AttendenceViewModel>> GetAllAsync(int? page, Filter filter, string search = null)
    {
        var attendences = unitOfWork.Attendences
            .SelectAsQueryable(includes: ["Lesson", "Student"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            attendences = attendences.Where(a =>
                a.Lesson.Name.ToLower().Contains(search.ToLower()) ||
                a.Student.FirstName.ToLower().Contains(search.ToLower()) ||
                a.Student.LastName.ToLower().Contains(search.ToLower()));

        return await (mapper.Map<IEnumerable<AttendenceViewModel>>(attendences)).ToPagedListAsync(page ?? 1, 5);
    }
}