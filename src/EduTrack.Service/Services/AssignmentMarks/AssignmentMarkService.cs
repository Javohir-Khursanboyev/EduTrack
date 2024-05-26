using AutoMapper;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Domain.Entities;
using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.AssignmentMarks;
using EduTrack.Service.DTOs.Assignments;
using EduTrack.Service.Exceptions;
using EduTrack.Service.Extensions;
using X.PagedList;

namespace EduTrack.Service.Services.AssignmentMarks;

public class AssignmentMarkService 
    (IMapper mapper, 
    IUnitOfWork unitOfWork) : IAssignmentMarkService
{
    public async ValueTask<AssignmentMarkViewModel> CreateAsync(AssignmentMarkCreateModel createModel)
    {
        var existAssignment = await unitOfWork.Assignments.SelectAsync(a => a.Id == createModel.AssignmentId)
            ?? throw new NotFoundException($"Assignment is not found");

        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == createModel.StudentId)
            ?? throw new NotFoundException($"Student is not found");

        var mappedAssignmentMark = mapper.Map<AssignmentMark>(createModel);
        mappedAssignmentMark.Create();

        var createdAssignmentMark = await unitOfWork.AssignmentMarks.InsertAsync(mappedAssignmentMark);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssignmentMarkViewModel>(createdAssignmentMark);
    }

    public async ValueTask<AssignmentMarkViewModel> UpdateAsync(long id, AssignmentMarkUpdateModel updateModel)
    {
        var existAssignmentMark = await unitOfWork.AssignmentMarks.SelectAsync(am => am.Id == id)
            ?? throw new NotFoundException($"AssignmentMark is not found");

        var existAssignment = await unitOfWork.Assignments.SelectAsync(a => a.Id == updateModel.AssignmentId)
            ?? throw new NotFoundException($"Assignment is not found");

        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == updateModel.StudentId)
            ?? throw new NotFoundException($"Student is not found");

        mapper.Map(updateModel, existAssignmentMark);
        existAssignmentMark.Update();
        var updatedAssignmentMark = await unitOfWork.AssignmentMarks.UpdateAsync(existAssignmentMark);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssignmentMarkViewModel>(updatedAssignmentMark);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existAssignmentMark = await unitOfWork.AssignmentMarks.SelectAsync(am => am.Id == id)
            ?? throw new NotFoundException($"AssignmentMark is not found");

        existAssignmentMark.Delete();
        await unitOfWork.AssignmentMarks.DeleteAsync(existAssignmentMark);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<AssignmentMarkViewModel> GetByIdAsync(long id)
    {
        var existAssignmentMark = await unitOfWork.AssignmentMarks.SelectAsync(am => am.Id == id, includes: ["Assignment", "Student"])
            ?? throw new NotFoundException($"AssignmentMark is not found");

        return mapper.Map<AssignmentMarkViewModel>(existAssignmentMark);
    }

    public async ValueTask<IPagedList<AssignmentMarkViewModel>> GetAllAsync(int? page, Filter filter, string search = null)
    {
        var assignmentMarks = unitOfWork.AssignmentMarks
            .SelectAsQueryable(includes: ["Assignment", "Student"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            assignmentMarks = assignmentMarks.Where(am =>
                am.Comment.ToLower().Contains(search.ToLower()));

        return await (mapper.Map<IEnumerable<AssignmentMarkViewModel>>(assignmentMarks)).ToPagedListAsync(page ?? 1, 5);
    }
}