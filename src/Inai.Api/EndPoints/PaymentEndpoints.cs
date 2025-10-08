using Inai.Api.Services;
using Inai.Core.Enums;
using Inai.Core.Models;

namespace Inai.Api.Endpoints;

public static class PaymentEndpoints
{
    public static void MapPayments(this WebApplication app)
    {
        app.MapGet("/payments/{userId:guid}", async (Guid userId, PaymentService service) =>
            Results.Ok(await service.GetUserPaymentsAsync(userId)));

        app.MapPost("/payment", async (Payment payment, PaymentService service) =>
        {
            var created = await service.CreatePaymentAsync(payment);
            return Results.Created($"/payment/{created.Id}", created);
        });

        app.MapPut("/payment/{id:guid}/status", async (Guid id, PaymentStatus status, PaymentService service) =>
            await service.UpdatePaymentStatusAsync(id, status) is Payment p ? Results.Ok(p) : Results.NotFound());
    }
}
