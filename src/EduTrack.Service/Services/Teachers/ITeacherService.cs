using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Teachers;
using X.PagedList;

namespace EduTrack.Service.Services.Students;

public interface ITeacherService
{
    ValueTask<TeacherViewModel> CreateAsync(TeacherCreateModel createModel);
    ValueTask<TeacherViewModel> UpdateAsync(long id, TeacherUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TeacherViewModel> GetByIdAsync(long id);
    ValueTask<IPagedList<TeacherViewModel>> GetAllAsync(int? page, Filter filter, string search = null);
}