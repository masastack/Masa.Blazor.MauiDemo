using CommunityToolkit.Maui.Core;
using Masa.Blazor.MauiDemo.Platforms;

namespace Masa.Blazor.MauiDemo;

public class MauiPlatformIntegration : IPlatformIntegration
{
    private readonly MasaBlazor _masaBlazor;

    public MauiPlatformIntegration(MasaBlazor masaBlazor)
    {
        _masaBlazor = masaBlazor;
        Microsoft.Maui.Controls.Application.Current.RequestedThemeChanged += CurrentOnRequestedThemeChanged;
    }

    public event EventHandler<int>? AppThemeChanged;

    private void CurrentOnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        Console.Out.WriteLine($"[MauiPlatformIntegration] CurrentOnRequestedThemeChanged {e.RequestedTheme}");

        var theme = (int)e.RequestedTheme;

        Preferences.Default.Set(AppThemeKey, theme);

        AppThemeChanged?.Invoke(sender, theme);
    }

    public async Task<GeoCoordinate?> GetCachedCoordinateAsync()
    {
        var location = await GetCachedLocationAsync();
        if (location is null)
        {
            return null;
        }

        return new GeoCoordinate()
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Altitude = location.Altitude
        };
    }

    public async Task<GeoCoordinate?> GetCurrentCoordinateAsync()
    {
        var location = await GetCurrentLocationAsync();
        if (location is null)
        {
            return null;
        }

        return new GeoCoordinate()
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Altitude = location.Altitude
        };
    }

    public void SetStatusBar(System.Drawing.Color color, int style)
    {
        throw new NotImplementedException();
    }

    // inherit
    public void SetStatusBar(string argb, int style)
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(23) || OperatingSystem.IsIOS())
        {
            CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(Color.FromArgb(argb));
            CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle((StatusBarStyle)style);
        }
    }

    private const string AppThemeKey = "AppTheme";

    public Task SetThemeAsync(int theme)
    {
        Microsoft.Maui.Controls.Application.Current.UserAppTheme = (AppTheme)theme;

        if (theme == 0)
        {
            var appTheme = Microsoft.Maui.Controls.Application.Current.RequestedTheme;
            _masaBlazor.SetTheme(appTheme == AppTheme.Dark);
        }
        else
        {
            _masaBlazor.SetTheme(theme == 2);
        }

        Preferences.Default.Set(AppThemeKey, theme);

        return Task.CompletedTask;
    }

    public Task InitThemeAsync()
    {
        var result = Preferences.Default.Get<int>(AppThemeKey, -1);
        var isDark = result < 1 ? Microsoft.Maui.Controls.Application.Current.RequestedTheme == AppTheme.Dark : result == 2;
        _masaBlazor.SetTheme(isDark);
        return Task.CompletedTask;
    }

    public ValueTask<int> GetThemeAsync()
    {
        var result = Preferences.Default.Get<int>(AppThemeKey, -1);
        return new ValueTask<int>(result == -1 ? (int)Microsoft.Maui.Controls.Application.Current.RequestedTheme : result);
    }

    public ValueTask<bool> IsDarkThemeOfSystemAsync()
    {
        return ValueTask.FromResult(Microsoft.Maui.Controls.Application.Current.RequestedTheme == AppTheme.Dark);
    }

    private async Task<Location?> GetCachedLocationAsync()
    {
        try
        {
            return await Geolocation.Default.GetLastKnownLocationAsync();
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Handle not supported on device exception
        }
        catch (FeatureNotEnabledException fneEx)
        {
            // Handle not enabled on device exception
        }
        catch (PermissionException pEx)
        {
            // Handle permission exception
        }
        catch (Exception ex)
        {
            // Unable to get location
        }

        return null;
    }

    private CancellationTokenSource? _cancelTokenSource;
    private bool _isCheckingLocation;

    private async Task<Location?> GetCurrentLocationAsync()
    {
        try
        {
            _isCheckingLocation = true;

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            _cancelTokenSource = new CancellationTokenSource();

            return await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
        }
        finally
        {
            _isCheckingLocation = false;
        }

        return null;
    }

    private void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}