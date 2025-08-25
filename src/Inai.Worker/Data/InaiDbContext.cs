using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inai.Worker.Data;

public class InaiDbContext : DbContext
{
    public InaiDbContext(DbContextOptions<InaiDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Reminder> Reminders => Set<Reminder>();
}
