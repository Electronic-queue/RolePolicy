namespace RolePolicy.Persistence;

public class DbInitializer
{
    public static void Initialize(RolePolicyDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
