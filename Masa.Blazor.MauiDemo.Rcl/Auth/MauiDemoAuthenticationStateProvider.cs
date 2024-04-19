using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Masa.Blazor.MauiDemo.JwtAuth;

public class MauiDemoAuthenticationStateProvider(UserService userService) : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await userService.GetAuthenticatedUserAsync();
        if (user?.Identity?.IsAuthenticated is true)
        {
            return new AuthenticationState(user);
        }

        // TODO: refresh token here?

        return new AuthenticationState(new ClaimsPrincipal());
    }

    public async Task<AuthenticationState> LoginAsync(string username, string password)
    {
        var user = await LoginWithExternalProviderAsync(username, password);
        _currentUser = user;
        var state = new AuthenticationState(_currentUser);
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }

    private Task<ClaimsPrincipal> LoginWithExternalProviderAsync(string username, string password)
    {
        /*
            Provide OpenID/MSAL code to authenticate the user. See your identity
            provider's documentation for details.

            Return a new ClaimsPrincipal based on a new ClaimsIdentity.
        */

        return userService.LoginAsync(username, password);
    }

    public void Logout()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        userService.Logout();
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(_currentUser)));
    }
}