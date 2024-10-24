using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IPaymentRepository
{
    void AddPayment(Payment payment);
    Task SaveChangesAsync();
}