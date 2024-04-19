using BlazorComponent;
using Masa.Blazor;
using Masa.Blazor.Presets;

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
                }
            };
            options.ConfigureTheme(theme =>
            {
                theme.Themes.Light.Primary = "#4f33ff";
                theme.Themes.Light.Secondary = "#5e5c71";
                // theme.Themes.Light.Accent = "#006C4F";
                theme.Themes.Light.Error = "#BA1A1A";
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
            });
        }, masaBlazorServiceLifetime);

        services.AddJwtAuthentication();

        return services;
    }
}