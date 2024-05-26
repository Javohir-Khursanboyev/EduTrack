using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Groups;
using X.PagedList;

namespace EduTrack.Domain.Entities;

public interface IGroupService
{
    ValueTask<GroupViewModel> CreateAsync(GroupCreateModel createModel);
    ValueTask<GroupViewModel> UpdateAsync(long id, GroupUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<GroupViewModel> GetByIdAsync(long id);
    ValueTask<IPagedList<GroupViewModel>> GetAllAsync(int? page, Filter filter, string search = null);
}