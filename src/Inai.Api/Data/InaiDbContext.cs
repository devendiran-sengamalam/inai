using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Data
{
    public class InaiDbContext : DbContext
    {
        public InaiDbContext(DbContextOptions<InaiDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; } = null!;
        public DbSet<Reminder> Reminders { get; set; } = null!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<HealthMetric> HealthMetrics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Task-Reminder relationship
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.TaskItem)
                .WithMany(t => t.Reminders)
                .HasForeignKey(r => r.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
