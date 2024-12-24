namespace RolePolicy.Persistence;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Surname { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();

    public virtual ICollection<RoleAccess> RoleAccessGivenByNavigations { get; set; } = new List<RoleAccess>();

    public virtual ICollection<RoleAccess> RoleAccessUsers { get; set; } = new List<RoleAccess>();

    public virtual ICollection<RoleResourceAction> RoleResourceActions { get; set; } = new List<RoleResourceAction>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
