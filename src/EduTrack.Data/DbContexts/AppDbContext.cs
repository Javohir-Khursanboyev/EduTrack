using EduTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Attendence> Attendences { get; set; }
    public DbSet<AssignmentMark> AssignmentMarks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>().HasQueryFilter(t => !t.IsDeleted);
        modelBuilder.Entity<Student>().HasQueryFilter(t => !t.IsDeleted);
        modelBuilder.Entity<Teacher>().HasQueryFilter(t => !t.IsDeleted);
        modelBuilder.Entity<Group>().HasQueryFilter(t => !t.IsDeleted);
        modelBuilder.Entity<Lesson>().HasQueryFilter(t => !t.IsDeleted);
        modelBuilder.Entity<Attendence>().HasQueryFilter(t => !t.IsDeleted);
        modelBuilder.Entity<Assignment>().HasQueryFilter(t => !t.IsDeleted);
        modelBuilder.Entity<AssignmentMark>().HasQueryFilter(t => !t.IsDeleted);
    }
}