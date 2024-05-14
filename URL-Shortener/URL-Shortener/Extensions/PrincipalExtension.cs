using System.Security.Claims;

namespace URL_Shortener.Extensions;

public static class PrincipalExtension
{
    public static Guid GetIdFromPrincipal(this ClaimsPrincipal principal)
    {
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        ArgumentNullException.ThrowIfNull(userId);

        return Guid.Parse(userId);
    }

    public static string GetRoleFromPrincipal(this ClaimsPrincipal principal)
    {
        var userRole = principal.FindFirstValue(ClaimTypes.Role);
        ArgumentNullException.ThrowIfNull(userRole);

        return userRole;
    }
}
