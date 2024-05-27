using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Students;
using X.PagedList;

namespace EduTrack.Service.Services.Students;

public interface IStudentService
{
    ValueTask<StudentViewModel> CreateAsync(StudentCreateModel createModel);
    ValueTask<StudentViewModel> UpdateAsync(long id, StudentUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<StudentViewModel> GetByIdAsync(long id);
    ValueTask<IPagedList<StudentViewModel>> GetAllAsync(int? page, string search = null);
}