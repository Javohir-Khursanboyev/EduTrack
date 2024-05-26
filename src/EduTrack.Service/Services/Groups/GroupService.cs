using AutoMapper;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Groups;
using EduTrack.Service.Exceptions;
using EduTrack.Service.Extensions;
using X.PagedList;

namespace EduTrack.Domain.Entities;

public class GroupService
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IGroupService
{
    public async ValueTask<GroupViewModel> CreateAsync(GroupCreateModel createModel)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == createModel.TeacherId)
            ?? throw new NotFoundException($"Teacher is not found");

        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Name.ToLower() == createModel.Name.ToLower());
        if (existGroup is not null)
            throw new AlreadyExistException($"Group is already exist with this Name = {createModel.Name}");

        var mappedGroup = mapper.Map<Group>(createModel);
        mappedGroup.Create();

        var createdGroup = await unitOfWork.Groups.InsertAsync(mappedGroup);
        await unitOfWork.SaveAsync();

        return mapper.Map<GroupViewModel>(createdGroup);
    }

    public async ValueTask<GroupViewModel> UpdateAsync(long id, GroupUpdateModel updateModel)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == updateModel.TeacherId)
           ?? throw new NotFoundException($"Teacher is not found");

        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == id)
            ?? throw new NotFoundException($"Group is not found");

        var alreadyExistGroup = await unitOfWork.Groups.SelectAsync(g => g.Name.ToLower() == updateModel.Name.ToLower() && g.Id != id);
        if (alreadyExistGroup is not null)
            throw new AlreadyExistException($"Group is already exist with this Name = {updateModel.Name}");

        mapper.Map(updateModel, existGroup);
        existGroup.Update();
        var updatedGroup = await unitOfWork.Groups.UpdateAsync(existGroup);
        await unitOfWork.SaveAsync();

        return mapper.Map<GroupViewModel>(updatedGroup);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == id)
            ?? throw new NotFoundException($"Group is not found");

        existGroup.Delete();
        await unitOfWork.Groups.DeleteAsync(existGroup);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<GroupViewModel> GetByIdAsync(long id)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == id, includes: ["Teacher", "Students", "Lessons", "Attendences"])
            ?? throw new NotFoundException($"Group is not found");

        return mapper.Map<GroupViewModel>(existGroup);
    }

    public async ValueTask<IPagedList<GroupViewModel>> GetAllAsync(int? page, Filter filter, string search = null)
    {
        var groups = unitOfWork.Groups
            .SelectAsQueryable(includes: ["Teacher", "Students", "Lessons", "Attendences"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            groups = groups.Where(g =>
                g.Name.ToLower().Contains(search.ToLower()));

        return await (mapper.Map<IEnumerable<GroupViewModel>>(groups)).ToPagedListAsync(page ?? 1, 5);
    }
}