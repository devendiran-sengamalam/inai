using Inai.Api.Services;
using Inai.Core.Models;

namespace Inai.Api.Endpoints;

public static class HealthEndpoints
{
    public static void MapHealth(this WebApplication app)
    {
        app.MapGet("/health/{userId:guid}", async (Guid userId, HealthService service) =>
            Results.Ok(await service.GetMetricsAsync(userId)));

        app.MapPost("/health", async (HealthMetric metric, HealthService service) =>
        {
            var created = await service.AddMetricAsync(metric);
            return Results.Created($"/health/{created.Id}", created);
        });
    }
}
