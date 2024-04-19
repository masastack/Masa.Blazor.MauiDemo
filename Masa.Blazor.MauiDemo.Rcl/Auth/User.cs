using System.Security.Claims;

namespace Masa.Blazor.MauiDemo.Rcl.Models;

public class User
{
    public string? Username { get; set; }

    public ClaimsPrincipal ToClaimsPrincipal()
        => new ClaimsPrincipal(
            new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Username) }, "Bearer"));

    public static User FromClaimsPrincipal(ClaimsPrincipal claimsPrincipal) => new User
    {
        Username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
    };
}