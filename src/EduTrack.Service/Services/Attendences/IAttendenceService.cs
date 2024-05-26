using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Attendences;
using X.PagedList;

namespace EduTrack.Service.Services.Attendences;

public interface IAttendenceService
{
    ValueTask<AttendenceViewModel> CreateAsync(AttendenceCreateModel createModel);
    ValueTask<AttendenceViewModel> UpdateAsync(long id, AttendenceUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AttendenceViewModel> GetByIdAsync(long id);
    ValueTask<IPagedList<AttendenceViewModel>> GetAllAsync(int? page, Filter filter, string search = null);
}