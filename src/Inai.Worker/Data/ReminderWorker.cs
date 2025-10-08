using Inai.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Inai.Worker;

public class ReminderWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ReminderWorker> _logger;

    public ReminderWorker(IServiceScopeFactory scopeFactory, ILogger<ReminderWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<InaiDbContext>();
            var now = DateTime.UtcNow;

            var reminders = await db.Reminders
                .Where(r => !r.IsSent && r.RemindAt <= now)
                .ToListAsync();

            foreach (var r in reminders)
            {
                // TODO: Send notification to user
                _logger.LogInformation($"Reminder for Task {r.TaskItemId}: {r.Message}");
                r.IsSent = true;
            }

            await db.SaveChangesAsync();
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // check every 30s
        }
    }
}

