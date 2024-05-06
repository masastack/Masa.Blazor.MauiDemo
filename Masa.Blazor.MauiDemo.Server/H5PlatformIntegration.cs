using BlazorComponent;
using Masa.Blazor.MauiDemo.Platforms;
using Microsoft.JSInterop;

namespace Masa.Blazor.MauiDemo.Server;

public class H5PlatformIntegration : IPlatformIntegration
{
    private readonly IJSRuntime _jsRuntime;
    private readonly LocalStorage _localStorage;
    private readonly MasaBlazor _masaBlazor;

    private GeoCoordinate? _cachedCoordinate;

    public H5PlatformIntegration(IJSRuntime jsRuntime, LocalStorage localStorage, MasaBlazor masaBlazor)
    {
        _jsRuntime = jsRuntime;
        _localStorage = localStorage;
        _masaBlazor = masaBlazor;
    }

    public Task<GeoCoordinate?> GetCachedCoordinateAsync()
    {
        return Task.FromResult(_cachedCoordinate);
    }

    public async Task<GeoCoordinate?> GetCurrentCoordinateAsync()
    {
        _cachedCoordinate = await _jsRuntime.InvokeAsync<GeoCoordinate>("__mauiDemo.getCurrentPosition");
        return _cachedCoordinate;
    }

    public void SetStatusBar(string argb, int style)
    {
        throw new NotImplementedException();
    }

    public async Task SetThemeAsync(int theme)
    {
        _ = _localStorage.SetItemAsync(AppThemeKey, theme);

        if (theme == 0)
        {
            var isDarkPreferColor = await IsDarkThemeOfSystemAsync();

            _masaBlazor.SetTheme(isDarkPreferColor);
        }
        else
        {
            _masaBlazor.SetTheme(theme == 2);
        }
    }

    private const string AppThemeKey = "AppTheme";

    public async Task InitThemeAsync()
    {
        var result = await _localStorage.GetItemAsync<int?>(AppThemeKey);
        var isDark = result is null or 0 ? await IsDarkThemeOfSystemAsync() : result == 2;
        _masaBlazor.SetTheme(isDark);
    }

    public async ValueTask<int> GetThemeAsync()
    {
        var result = await _localStorage.GetItemAsync<int?>(AppThemeKey);
        return result ?? 0;
    }

    public async ValueTask<bool> IsDarkThemeOfSystemAsync()
    {
        return await _jsRuntime.InvokeAsync<bool>("eval", "window.matchMedia('(prefers-color-scheme: dark)').matches");
    }

    public async Task SetCacheAsync<TValue>(string key, TValue value)
    {
        await _localStorage.SetItemAsync(key, value);
    }

    public async Task<TValue> GetCacheAsync<TValue>(string key, TValue defaultValue)
    {
        var result = await _localStorage.GetItemAsync<TValue>(key);
        return result ?? defaultValue;
    }

    public Task RemoveCacheAsync(string key)
    {
        return _localStorage.RemoveItemAsync(key);
    }
}