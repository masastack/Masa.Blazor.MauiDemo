using System.Security.Claims;
using Masa.Blazor.MauiDemo.Rcl.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Masa.Blazor.MauiDemo.Rcl.Auth;

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

    public async Task<AuthenticationState> LoginViaSmsAsync(string phoneNumber, string confirmCode)
    {
        var user = await LoginViaSmsWithExternalProviderAsync(phoneNumber, confirmCode);
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

    private Task<ClaimsPrincipal> LoginViaSmsWithExternalProviderAsync(string phoneNumber, string confirmCode)
    {
        /*
            Provide OpenID/MSAL code to authenticate the user. See your identity
            provider's documentation for details.

            Return a new ClaimsPrincipal based on a new ClaimsIdentity.
        */

        return userService.LoginBySmsAsync(phoneNumber, confirmCode);
    }

    public void Logout()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        userService.Logout();
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(_currentUser)));
    }
}