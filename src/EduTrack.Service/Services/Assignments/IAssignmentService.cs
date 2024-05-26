using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Assignments;
using X.PagedList;

namespace EduTrack.Service.Services.Assignments;

public interface IAssignmentService
{
    ValueTask<AssignmentViewModel> CreateAsync(AssignmentCreateModel createModel);
    ValueTask<AssignmentViewModel> UpdateAsync(long id, AssignmentUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AssignmentViewModel> GetByIdAsync(long id);
    ValueTask<IPagedList<AssignmentViewModel>> GetAllAsync(int? page, Filter filter, string search = null);
}