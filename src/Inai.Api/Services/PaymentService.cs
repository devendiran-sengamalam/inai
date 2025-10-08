using Inai.Api.Data;
using Inai.Core.Enums;
using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Services;

public class PaymentService
{
    private readonly InaiDbContext _db;
    public PaymentService(InaiDbContext db) => _db = db;

    public async Task<Payment> CreatePaymentAsync(Payment payment)
    {
        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment?> UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus status)
    {
        var payment = await _db.Payments.FindAsync(paymentId);
        if (payment == null) return null;

        payment.Status = status;
        await _db.SaveChangesAsync();
        return payment;
    }

    public async Task<IEnumerable<Payment>> GetUserPaymentsAsync(Guid userId) =>
        await _db.Payments.Where(p => p.UserId == userId).ToListAsync();
}
