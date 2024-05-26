using AutoMapper;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Domain.Entities;
using EduTrack.Service.Configurations;
using EduTrack.Service.DTOs.Teachers;
using EduTrack.Service.Exceptions;
using EduTrack.Service.Extensions;
using EduTrack.Service.Helpers;
using EduTrack.Service.Services.Students;
using X.PagedList;

namespace EduTrack.Service.Services.Teachers;

public class TeacherService 
    (IMapper mapper,
    IUnitOfWork unitOfWork) : ITeacherService
{
    public async ValueTask<TeacherViewModel> CreateAsync(TeacherCreateModel createModel)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Email == createModel.Email);
        if (existTeacher is not null)
            throw new AlreadyExistException($"Teacher is already exist with thi Email = {createModel.Email}");

        var mappedTeacher = mapper.Map<Teacher>(createModel);
        mappedTeacher.PasswordHash = PasswordHasher.Hash(createModel.Password);
        mappedTeacher.Create();

        var createdTeacher = await unitOfWork.Teachers.InsertAsync(mappedTeacher);
        await unitOfWork.SaveAsync();

        return mapper.Map<TeacherViewModel>(createdTeacher);
    }

    public async ValueTask<TeacherViewModel> UpdateAsync(long id, TeacherUpdateModel updateModel)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"Teacher is not found");

        var alreadyExisttTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Email == updateModel.Email && t.Id != id);
        if (alreadyExisttTeacher is not null)
            throw new AlreadyExistException($"Teacher is already exist with this Email = {updateModel.Email}");

        mapper.Map(updateModel, existTeacher);
        existTeacher.Update();
        var updatedTeacher = await unitOfWork.Teachers.UpdateAsync(existTeacher);
        await unitOfWork.SaveAsync();

        return mapper.Map<TeacherViewModel>(updatedTeacher);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == id)
             ?? throw new NotFoundException($"Teacher is not found");

        existTeacher.Delete();
        await unitOfWork.Teachers.DeleteAsync(existTeacher);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<TeacherViewModel> GetByIdAsync(long id)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == id, ["Group"])
           ?? throw new NotFoundException($"Teacher is not found");

        return mapper.Map<TeacherViewModel>(existTeacher);
    }

    public async ValueTask<IPagedList<TeacherViewModel>> GetAllAsync(int? page, Filter filter, string search = null)
    {
        var teachers = unitOfWork.Teachers
           .SelectAsQueryable(includes: ["Group"], isTracked: false)
           .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            teachers = teachers.Where(t =>
                t.FirstName.ToLower().Contains(search.ToLower()) ||
                t.LastName.Contains(search.ToLower()));

        return await(mapper.Map<IEnumerable<TeacherViewModel>>(teachers)).ToPagedListAsync(page ?? 1, 5);
    }
}