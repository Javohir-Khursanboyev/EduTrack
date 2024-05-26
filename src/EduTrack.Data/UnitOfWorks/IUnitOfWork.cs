using EduTrack.Data.Repositories;
using EduTrack.Domain.Entities;

namespace EduTrack.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Student> Students { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Teacher> Teachers { get; }
    IRepository<Lesson> Lessons { get; }
    IRepository<Group> Groups { get; }
    IRepository<Attendence> Attendences { get; }
    IRepository<Assignment> Assignments { get; }
    IRepository<AssignmentMark> AssignmentMarks { get; }
    ValueTask<bool> SaveAsync();
    ValueTask BeginTransactionAsync();
    ValueTask CommitTransactionAsync();
}