using System;

namespace Inai.Core.Models;

public class HealthMetric
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string MetricName { get; set; } = string.Empty; // e.g., "HeartRate", "Steps"
    public double Value { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}
