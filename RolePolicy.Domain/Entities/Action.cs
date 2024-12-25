namespace RolePolicy.Domain.Entities;

public partial class Action
{
    public int ActionId { get; set; }

    public string Name { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<RoleResourceAction> RoleResourceActions { get; set; } = new List<RoleResourceAction>();
}
