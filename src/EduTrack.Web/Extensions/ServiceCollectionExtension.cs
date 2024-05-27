using EduTrack.Data.Repositories;
using EduTrack.Data.UnitOfWorks;
using EduTrack.Service.Services.Students;
using EduTrack.Service.Services.Teachers;

namespace EduTrack.Web.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();
    }
}