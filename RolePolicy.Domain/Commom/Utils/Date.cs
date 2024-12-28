namespace RolePolicy.Domain.Commom.Utils;

public static class Date
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public static DateTime GetDate()
    {
        return DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime;
    }
}
