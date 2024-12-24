namespace RolePolicy.Persistence;

public partial class RoleAccess
{
    public int RoleAccessId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? GivenBy { get; set; }

    public virtual User? GivenByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
