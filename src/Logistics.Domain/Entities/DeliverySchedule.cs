namespace Logistics.Domain.Entities;

public class DeliverySchedule : BaseEntity
{
    public int OrderId { get; set; }
    public required Order Order { get; set; }
    public DateTime EstimatedLoadingDate { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime ActualLoadingDate { get; set; }
    public DateTime ActualDeliveryDate { get; set; }
}