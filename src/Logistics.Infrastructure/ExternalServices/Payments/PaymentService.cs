using Logistics.Application.Interfaces.Payments;
using Logistics.Domain.Entities.Payments;

namespace Logistics.Infrastructure.ExternalServices.Payments;

public class PaymentService : IPaymentService
{
    public int InitPayment(Payment payment)
    {
        var random = new Random();
        return random.Next(1, (int)payment.Amount.Sum);
    }
}