namespace RolePolicy.Persistence;

public partial class RoleResourceAction
{
    public int RoleResourceActionId { get; set; }

    public int RoleId { get; set; }

    public int ResourceId { get; set; }

    public int ActionId { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Resource Resource { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
