using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Data;

public class InaiDbContext : DbContext
{
    public InaiDbContext(DbContextOptions<InaiDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Reminder> Reminders => Set<Reminder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany<TaskItem>()
            .WithOne(t => t.User!)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<TaskItem>()
            .HasMany<Reminder>()
            .WithOne(r => r.TaskItem!)
            .HasForeignKey(r => r.TaskItemId);
    }
}
