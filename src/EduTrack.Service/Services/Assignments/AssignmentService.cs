using AutoMapper;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Domain.Entities;
using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Assignments;
using EduTrack.Service.Exceptions;
using EduTrack.Service.Extensions;
using X.PagedList;

namespace EduTrack.Service.Services.Assignments;

public class AssignmentService
    (IMapper mapper, 
    IUnitOfWork unitOfWork) : IAssignmentService
{
    public async ValueTask<AssignmentViewModel> CreateAsync(AssignmentCreateModel createModel)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == createModel.GroupId)
            ?? throw new NotFoundException($"Group is not found");

        var mappedAssignment = mapper.Map<Assignment>(createModel);
        mappedAssignment.Create();

        var createdAssignment = await unitOfWork.Assignments.InsertAsync(mappedAssignment);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssignmentViewModel>(createdAssignment);
    }

    public async ValueTask<AssignmentViewModel> UpdateAsync(long id, AssignmentUpdateModel updateModel)
    {
        var existAssignment = await unitOfWork.Assignments.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException($"Assignment is not found");

        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == updateModel.GroupId)
            ?? throw new NotFoundException($"Group is not found");

        mapper.Map(updateModel, existAssignment);
        existAssignment.Update();
        var updatedAssignment = await unitOfWork.Assignments.UpdateAsync(existAssignment);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssignmentViewModel>(updatedAssignment);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existAssignment = await unitOfWork.Assignments.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException($"Assignment is not found");

        existAssignment.Delete();
        await unitOfWork.Assignments.DeleteAsync(existAssignment);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<AssignmentViewModel> GetByIdAsync(long id)
    {
        var existAssignment = await unitOfWork.Assignments.SelectAsync(a => a.Id == id, includes: ["Group", "Asset", "AssignmentMarks"])
            ?? throw new NotFoundException($"Assignment is not found");

        return mapper.Map<AssignmentViewModel>(existAssignment);
    }

    public async ValueTask<IPagedList<AssignmentViewModel>> GetAllAsync(int? page, Filter filter, string search = null)
    {
        var assignments = unitOfWork.Assignments
            .SelectAsQueryable(includes: ["Group", "Asset", "AssignmentMarks"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            assignments = assignments.Where(a =>
                a.Name.ToLower().Contains(search.ToLower()) ||
                a.Description.ToLower().Contains(search.ToLower()));

        return await (mapper.Map<IEnumerable<AssignmentViewModel>>(assignments)).ToPagedListAsync(page ?? 1, 5);
    }
}