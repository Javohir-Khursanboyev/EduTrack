using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.AssignmentMarks;
using EduTrack.Service.DTOs.Assignments;
using X.PagedList;

namespace EduTrack.Service.Services.AssignmentMarks;

public interface IAssignmentMarkService
{
    ValueTask<AssignmentMarkViewModel> CreateAsync(AssignmentMarkCreateModel createModel);
    ValueTask<AssignmentMarkViewModel> UpdateAsync(long id, AssignmentMarkUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AssignmentMarkViewModel> GetByIdAsync(long id);
    ValueTask<IPagedList<AssignmentMarkViewModel>> GetAllAsync(int? page, Filter filter, string search = null);
}