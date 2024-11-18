using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackerApp.DataAccess.Configurations;
using TrackerApp.DataAccess.Entities;

namespace TrackerApp.DataAccess;

public class TaskDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TaskEntity> Tasks { get; set; }
}
