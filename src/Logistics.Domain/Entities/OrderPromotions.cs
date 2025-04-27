namespace Logistics.Domain.Entities;

public class OrderPromotions : BaseEntity
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int PromotionId { get; set; }
    public Promotion? Promotion { get; set; }
}