using Inai.Worker.Data;
using Microsoft.EntityFrameworkCore;

namespace Inai.Worker
{
    public class ReminderWorker : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ReminderWorker> _logger;

        public ReminderWorker(IServiceProvider services, ILogger<ReminderWorker> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<InaiDbContext>();

                var now = DateTime.UtcNow;
                var dueReminders = await db.Reminders
                    .Where(r => r.RemindAt <= now)
                    .ToListAsync(stoppingToken);

                foreach (var reminder in dueReminders)
                {
                    // TODO later: Send SignalR notification to App

                    db.Reminders.Remove(reminder);
                }

                await db.SaveChangesAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
