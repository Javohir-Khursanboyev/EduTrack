using Microsoft.EntityFrameworkCore;

namespace EduTrack.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {   }
}