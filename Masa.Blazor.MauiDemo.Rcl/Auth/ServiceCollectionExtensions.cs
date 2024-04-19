using Masa.Blazor.MauiDemo.JwtAuth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions2
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationDataMemoryStorage>();
        services.AddScoped<UserService>();
        services.AddScoped<MauiDemoAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(provider =>
            provider.GetRequiredService<MauiDemoAuthenticationStateProvider>());
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();
    }
}