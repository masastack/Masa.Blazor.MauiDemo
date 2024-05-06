using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorComponent;
using Masa.Blazor.MauiDemo.Platforms;

namespace Masa.Blazor.MauiDemo.Rcl.Services;

public class UserService
{
    private readonly IPopupService _popupService;
    private readonly IPlatformIntegration _platformIntegration;

    public UserService(IPopupService popupService, IPlatformIntegration platformIntegration)
    {
        _popupService = popupService;
        _platformIntegration = platformIntegration;
    }

    public async Task<ClaimsPrincipal> LoginAsync(string username, string password)
    {
        // Simulate a login request
        {
            await Task.Delay(2000);

            if (username == "test" && password == "test")
            {
                // In a real-world scenario, you would call an API to get the token

                // fake token
                var accessToken =
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImlzcyI6Ik1hdWlEZW1vIiwiYXVkIjoiTWF1aURlbW8ifQ.mX1z0dTbK8vLDUxlul11IzyhDEvnunlD-LnMIDqKsG8";

                // JWT has a set of predefined claims, such as sub, iss, aud, exp, and others.
                // exp is the expiration time of the token, and it is a Unix timestamp.
                // Because the token is fake, and it doesn't have a real expiration time,
                // so we fake it here.

                var expiresOn = TimeSpan.FromMinutes(1);
                var expiredAt = DateTime.UtcNow.Add(expiresOn);

                var token = new JwtToken(accessToken, expiredAt);
                PersistToken(token);
                var claimPrincipal = CreateClaimsPrincipalFromToken(accessToken);
                return claimPrincipal;
            }
        }

        await _popupService.EnqueueSnackbarAsync("Login failed", "Invalid username or password", AlertTypes.Error);

        return new ClaimsPrincipal();
    }

    public async Task<ClaimsPrincipal> LoginBySmsAsync(string phoneNumber, string password)
    {
        // Simulate a login request
        return await LoginAsync("test", "test");
    }
    
    public async Task<bool> ValidateConfirmCodeAsync(string phoneNumber, string confirmCode)
    {
        // Simulate a validate confirm code request
        await Task.Delay(1000);
        return await Task.FromResult(true);
    }

    public async Task<bool> ResetPasswordAsync(string phoneNumber, string password, string confirmCode)
    {
        // Simulate a reset password request
        await Task.Delay(1000);
        return await Task.FromResult(true);
    }

    public async Task<ClaimsPrincipal?> GetAuthenticatedUserAsync()
    {
        var jwtToken = await _platformIntegration.GetCacheAsync<JwtToken?>("jwt_token", null);
        if (jwtToken is not null)
        {
            if (jwtToken.ExpiredAt > DateTime.UtcNow)
            {
                return CreateClaimsPrincipalFromToken(jwtToken.AccessToken);
            }

            await _popupService.EnqueueSnackbarAsync("Login expired", "Please login again", AlertTypes.Error);
        }

        return null;
    }

    public void Logout()
    {
        _ = _platformIntegration.RemoveCacheAsync("jwt_token");
    }

    private void PersistToken(JwtToken token)
    {
        _ = _platformIntegration.SetCacheAsync("jwt_token", token);
    }

    private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var identity = new ClaimsIdentity();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            identity = new ClaimsIdentity(jwtSecurityToken.Claims, "Bearer");
        }

        return new ClaimsPrincipal(identity);
    }

    record JwtToken(string AccessToken, DateTime ExpiredAt);
}