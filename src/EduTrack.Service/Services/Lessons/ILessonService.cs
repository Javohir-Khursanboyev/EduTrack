using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Lessons;
using X.PagedList;

namespace EduTrack.Service.Services.Lessons;

public interface ILessonService
{
    ValueTask<LessonViewModel> CreateAsync(LessonCreateModel createModel);
    ValueTask<LessonViewModel> UpdateAsync(long id, LessonUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<LessonViewModel> GetByIdAsync(long id);
    ValueTask<IPagedList<LessonViewModel>> GetAllAsync(int? page, Filter filter, string search = null);
}