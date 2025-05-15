using Logistics.Domain.Entities.Payments;

namespace Logistics.Application.Interfaces.Payments;

public interface IPaymentService
{
    int InitPayment(Payment payment);
}