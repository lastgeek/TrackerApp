using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackerApp.DataAccess.Entities;
using TrackerApp.Domain.Models;

namespace TrackerApp.DataAccess.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(TaskModel.MAX_TITLE_LENGTH);

        builder.Property(t => t.Description)
            .HasMaxLength(TaskModel.MAX_DISCRIPTION_LENGTH);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasMaxLength(TaskModel.MAX_STATUS_LENGTH);

        builder.Property(t => t.Priority)
            .IsRequired()
            .HasMaxLength(TaskModel.MAX_PRIORITY_LENGTH);

        builder.HasOne(t => t.AssignedUser)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.AssignedUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
