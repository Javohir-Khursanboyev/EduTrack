using AutoMapper;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Domain.Entities;
using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Students;
using EduTrack.Service.Exceptions;
using EduTrack.Service.Extensions;
using EduTrack.Service.Helpers;
using X.PagedList;

namespace EduTrack.Service.Services.Students;

public class StudentService
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IStudentService
{
    public async ValueTask<StudentViewModel> CreateAsync(StudentCreateModel createModel)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Phone == createModel.Phone);
        if (existStudent is not null)
            throw new AlreadyExistException($"Student is already exist with thi Phone = {createModel.Phone}");

        var mappedStudent = mapper.Map<Student>(createModel);
        mappedStudent.PasswordHash = PasswordHasher.Hash(createModel.Password);
        mappedStudent.Create();

        var createdStudent = await unitOfWork.Students.InsertAsync(mappedStudent);
        await unitOfWork.SaveAsync();

        return mapper.Map<StudentViewModel>(createdStudent);
    }

    public async ValueTask<StudentViewModel> UpdateAsync(long id, StudentUpdateModel updateModel)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == id)
            ?? throw new NotFoundException($"Student is not found");

        var alreadyExistStudent = await unitOfWork.Students.SelectAsync(s => s.Phone == updateModel.Phone && s.Id != id);
        if (alreadyExistStudent is not null)
            throw new AlreadyExistException($"Student is already exist with thi Phone = {updateModel.Phone}");

        mapper.Map(updateModel, existStudent);
        existStudent.Update();
        var updatedStudent = await unitOfWork.Students.UpdateAsync(existStudent);
        await unitOfWork.SaveAsync();

        return mapper.Map<StudentViewModel>(updatedStudent);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == id)
            ?? throw new NotFoundException($"Student is not found");

        existStudent.Delete();
        await unitOfWork.Students.DeleteAsync(existStudent);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<StudentViewModel> GetByIdAsync(long id)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == id, ["Group", "AssignmentMarks"])
            ?? throw new NotFoundException($"Student is not found");

        return mapper.Map<StudentViewModel>(existStudent);
    }

    public async ValueTask<IPagedList<StudentViewModel>> GetAllAsync(int? page, Filter filter, string search = null)
    {
        var students = unitOfWork.Students
            .SelectAsQueryable(includes: ["Group", "AssignmentMarks"], isTracked: false)
            .OrderBy(filter);

        if(!string.IsNullOrWhiteSpace(search))
            students = students.Where(s => 
                s.FirstName.ToLower().Contains(search.ToLower()) ||
                s.LastName.Contains(search.ToLower()));

        return await (mapper.Map<IEnumerable<StudentViewModel>>(students)).ToPagedListAsync(page ?? 1, 5);
    }
}