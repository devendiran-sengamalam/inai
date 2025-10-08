using Inai.Api.Data;
using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Services;

public class HealthService
{
    private readonly InaiDbContext _db;
    public HealthService(InaiDbContext db) => _db = db;

    public async Task<HealthMetric> AddMetricAsync(HealthMetric metric)
    {
        _db.HealthMetrics.Add(metric);
        await _db.SaveChangesAsync();
        return metric;
    }

    public async Task<IEnumerable<HealthMetric>> GetMetricsAsync(Guid userId) =>
        await _db.HealthMetrics.Where(h => h.UserId == userId).OrderBy(h => h.RecordedAt).ToListAsync();
}
