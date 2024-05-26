using Arcana.DataAccess.Repositories;
using EduTrack.Data.DbContexts;
using EduTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Arcana.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
   
    private IDbContextTransaction transaction;

    public IRepository<Student> Students { get; }

    public IRepository<Asset> Assets { get; }

    public IRepository<Teacher> Teachers { get; }

    public IRepository<Lesson> Lessons { get; }

    public IRepository<Group> Groups { get; }

    public IRepository<Attendence> Attendences { get; }

    public IRepository<Assignment> Assignments { get; }

    public IRepository<AssignmentMark> AssignmentMarks { get; }

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Students = new Repository<Student>(this.context);
        Assets = new Repository<Asset>(this.context);
        Teachers = new Repository<Teacher>(this.context);
        Lessons = new Repository<Lesson>(this.context);
        Groups = new Repository<Group>(this.context);
        Attendences = new Repository<Attendence>(this.context);
        Assignments = new Repository<Assignment>(this.context);
        AssignmentMarks = new Repository<AssignmentMark>(this.context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async ValueTask BeginTransactionAsync()
    {
        transaction = await this.context.Database.BeginTransactionAsync();
    }

    public async ValueTask CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }
}
