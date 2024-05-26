using AutoMapper;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Domain.Entities;
using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Lessons;
using EduTrack.Service.Exceptions;
using EduTrack.Service.Extensions;
using X.PagedList;

namespace EduTrack.Service.Services.Lessons;

public class LessonService
    (IMapper mapper, 
    IUnitOfWork unitOfWork) : ILessonService
{
    public async ValueTask<LessonViewModel> CreateAsync(LessonCreateModel createModel)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == createModel.GroupId)
            ?? throw new NotFoundException($"Group is not found");

        var mappedLesson = mapper.Map<Lesson>(createModel);
        mappedLesson.Create();

        var createdLesson = await unitOfWork.Lessons.InsertAsync(mappedLesson);
        await unitOfWork.SaveAsync();

        return mapper.Map<LessonViewModel>(createdLesson);
    }

    public async ValueTask<LessonViewModel> UpdateAsync(long id, LessonUpdateModel updateModel)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == updateModel.GroupId)
            ?? throw new NotFoundException($"Group is not found");

        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson is not found");

        mapper.Map(updateModel, existLesson);
        existLesson.Update();
        var updatedLesson = await unitOfWork.Lessons.UpdateAsync(existLesson);
        await unitOfWork.SaveAsync();

        return mapper.Map<LessonViewModel>(updatedLesson);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson is not found");

        existLesson.Delete();
        await unitOfWork.Lessons.DeleteAsync(existLesson);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<LessonViewModel> GetByIdAsync(long id)
    {
        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id, includes: ["Group", "Attendences"])
            ?? throw new NotFoundException($"Lesson is not found");

        return mapper.Map<LessonViewModel>(existLesson);
    }

    public async ValueTask<IPagedList<LessonViewModel>> GetAllAsync(int? page, Filter filter, string search = null)
    {
        var lessons = unitOfWork.Lessons
            .SelectAsQueryable(includes: ["Group", "Attendences"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            lessons = lessons.Where(l =>
                l.Name.ToLower().Contains(search.ToLower()) ||
                l.Description.ToLower().Contains(search.ToLower()));

        return await (mapper.Map<IEnumerable<LessonViewModel>>(lessons)).ToPagedListAsync(page ?? 1, 5);
    }
}