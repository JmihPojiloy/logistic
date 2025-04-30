namespace Logistics.Domain.Entities;

public class Promotion : BaseEntity
{
    public int Code {get; set;}
    public required string Description {get; set;}
    public int? Discount {get; set;}
    public DateTime? StartDate {get; set;}
    public DateTime? EndDate {get; set;}
    public virtual ICollection<OrderPromotion> OrderPromotions { get; set; } = new List<OrderPromotion>();
}