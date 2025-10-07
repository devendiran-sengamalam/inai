using Microsoft.EntityFrameworkCore;
using Inai.Core.Models;

namespace Inai.Api.Data
{
    public class InaiDbContext : DbContext
    {
        public InaiDbContext(DbContextOptions<InaiDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; } = null!;
        public DbSet<Reminder> Reminders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.TaskItem)
                .WithMany(t => t.Reminders)
                .HasForeignKey(r => r.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
