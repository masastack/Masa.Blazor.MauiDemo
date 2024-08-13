using Masa.Blazor;
using Masa.Blazor.MauiDemo.Rcl.Auth;
using Masa.Blazor.MauiDemo.Rcl.Services;
using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasaBlazorMauiDemo(this IServiceCollection services,
        ServiceLifetime masaBlazorServiceLifetime = ServiceLifetime.Scoped)
    {
        services.AddMasaBlazor(options =>
        {
            options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
            {
                ["MBottomSheet"] = new Dictionary<string, object?>()
                {
                    ["ContentClass"] = "pa-4"
                },
                ["MButton"] = new Dictionary<string, object?>()
                {
                    [nameof(MButton.Depressed)] = true
                },
                ["MImage"] = new Dictionary<string, object?>()
                {
                    // https://github.com/masastack/MASA.Blazor/issues/1624
                    [nameof(MImage.Eager)] = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS()
                },
                ["MSheet"] = new Dictionary<string, object?>()
                {
                    ["Rounded"] = (StringBoolean)true,
                },
                ["MTextField"] = new Dictionary<string, object?>()
                {
                    ["Filled"] = true,
                    ["Rounded"] = true,
                    ["PersistentPlaceholder"] = true
                },
                ["MTextarea"] = new Dictionary<string, object?>()
                {
                    ["Filled"] = true,
                    ["Rounded"] = true,
                    ["PersistentPlaceholder"] = true
                },
                [PopupComponents.SNACKBAR] = new Dictionary<string, object?>()
                {
                    { nameof(PEnqueuedSnackbars.Closeable), true },
                    { nameof(PEnqueuedSnackbars.Text), true }
                },
                ["PStackPageBarInit"] = new Dictionary<string, object?>()
                {
                    { nameof(PStackPageBarInit.CenterTitle), true },
                    { nameof(PStackPageBarInit.Flat), true }
                }
            };
            options.ConfigureTheme(theme =>
            {
                theme.Themes.Light.Primary = "#4f33ff";
                theme.Themes.Light.Secondary = "#5e5c71";
                // theme.Themes.Light.Accent = "#006C4F";
                theme.Themes.Light.Error = "#BA1A1A";
                theme.Themes.Light.Surface = "#f0f3fa";
                theme.Themes.Light.OnSurface = "#1C1B1F";
                theme.Themes.Light.InverseSurface = "#131316";
                theme.Themes.Light.InverseOnSurface = "#C9C5CA";
                theme.Themes.Light.InversePrimary = "#C5C0FF";

                theme.Themes.Dark.Primary = "#C5C0FF";
                // theme.Themes.Dark.OnPrimary = "#090029";
                theme.Themes.Dark.Secondary = "#C7C4DC";
                theme.Themes.Dark.OnSecondary = "#302E42";
                theme.Themes.Dark.Accent = "#67DBAF";
                theme.Themes.Dark.OnAccent = "#003827";
                theme.Themes.Dark.Error = "#FFB4AB";
                theme.Themes.Dark.OnError = "#690005";
                theme.Themes.Dark.Surface = "#131316";
                theme.Themes.Dark.OnSurface = "#C9C5CA";
                theme.Themes.Dark.InverseOnSurface = "#1C1B1F";
                theme.Themes.Dark.InversePrimary = "#4f33ff";
            });
            options.ConfigureIcons(IconSet.MaterialDesignIcons, aliases =>
            {
                aliases.UserDefined["wechat"] = new SvgPath(
                    "M9.5,4C5.36,4 2,6.69 2,10C2,11.89 3.08,13.56 4.78,14.66L4,17L6.5,15.5C7.39,15.81 8.37,16 9.41,16C9.15,15.37 9,14.7 9,14C9,10.69 12.13,8 16,8C16.19,8 16.38,8 16.56,8.03C15.54,5.69 12.78,4 9.5,4M6.5,6.5A1,1 0 0,1 7.5,7.5A1,1 0 0,1 6.5,8.5A1,1 0 0,1 5.5,7.5A1,1 0 0,1 6.5,6.5M11.5,6.5A1,1 0 0,1 12.5,7.5A1,1 0 0,1 11.5,8.5A1,1 0 0,1 10.5,7.5A1,1 0 0,1 11.5,6.5M16,9C12.69,9 10,11.24 10,14C10,16.76 12.69,19 16,19C16.67,19 17.31,18.92 17.91,18.75L20,20L19.38,18.13C20.95,17.22 22,15.71 22,14C22,11.24 19.31,9 16,9M14,11.5A1,1 0 0,1 15,12.5A1,1 0 0,1 14,13.5A1,1 0 0,1 13,12.5A1,1 0 0,1 14,11.5M18,11.5A1,1 0 0,1 19,12.5A1,1 0 0,1 18,13.5A1,1 0 0,1 17,12.5A1,1 0 0,1 18,11.5Z");
                aliases.UserDefined["apple"] = new SvgPath(
                    "M18.71,19.5C17.88,20.74 17,21.95 15.66,21.97C14.32,22 13.89,21.18 12.37,21.18C10.84,21.18 10.37,21.95 9.1,22C7.79,22.05 6.8,20.68 5.96,19.47C4.25,17 2.94,12.45 4.7,9.39C5.57,7.87 7.13,6.91 8.82,6.88C10.1,6.86 11.32,7.75 12.11,7.75C12.89,7.75 14.37,6.68 15.92,6.84C16.57,6.87 18.39,7.1 19.56,8.82C19.47,8.88 17.39,10.1 17.41,12.63C17.44,15.65 20.06,16.66 20.09,16.67C20.06,16.74 19.67,18.11 18.71,19.5M13,3.5C13.73,2.67 14.94,2.04 15.94,2C16.07,3.17 15.6,4.35 14.9,5.19C14.21,6.04 13.07,6.7 11.95,6.61C11.8,5.46 12.36,4.26 13,3.5Z");
                aliases.UserDefined["google"] = new SvgPath(
                    "M21.35,11.1H12.18V13.83H18.69C18.36,17.64 15.19,19.27 12.19,19.27C8.36,19.27 5,16.25 5,12C5,7.9 8.2,4.73 12.2,4.73C15.29,4.73 17.1,6.7 17.1,6.7L19,4.72C19,4.72 16.56,2 12.1,2C6.42,2 2.03,6.8 2.03,12C2.03,17.05 6.16,22 12.25,22C17.6,22 21.5,18.33 21.5,12.91C21.5,11.76 21.35,11.1 21.35,11.1V11.1Z");
            });
        }, masaBlazorServiceLifetime);

        services.AddScoped<UserService>();
        services.AddScoped<MauiDemoAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(provider =>
            provider.GetRequiredService<MauiDemoAuthenticationStateProvider>());
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();

        return services;
    }
}