using Domain.Entities;
using Persistence;

namespace Infrastructure.Repositories;

public class PaymentRepository(IDbContext context) : IPaymentRepository
{
    public void AddPayment(Payment payment)
    {
        context.Payments.Add(payment);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}