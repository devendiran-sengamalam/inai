using Inai.Worker;
using Inai.Worker.Data;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<InaiDbContext>(options =>
            options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddHostedService<ReminderWorker>();
    })
    .Build();

await host.RunAsync();
