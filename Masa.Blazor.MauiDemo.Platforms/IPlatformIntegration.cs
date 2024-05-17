namespace Masa.Blazor.MauiDemo.Platforms;

public interface IPlatformIntegration
{
    /// <summary>
    /// 测试用 记得删除/重写
    /// </summary>
    /// <remarks>
    /// <para> true => 全屏 </para>
    /// <para> false => 取消全屏 状态栏green 导航栏 blue</para>
    /// </remarks>
    void FullWVTest(bool tf);
    Task<GeoCoordinate?> GetCachedCoordinateAsync();

    Task<GeoCoordinate?> GetCurrentCoordinateAsync();

    /// <summary>
    /// Set the status bar color and style
    /// </summary>
    /// <param name="argb">The color in ARGB format</param>
    /// <param name="style">0: default, 1: light content, 2: dark content</param>
    void SetStatusBar(string argb, int style);

    /// <summary>
    /// Set app theme
    /// </summary>
    /// <param name="theme">0: auto, 1: light, 2: dark</param>
    Task SetThemeAsync(int theme);

    /// <summary>
    /// Init app theme
    /// </summary>
    Task InitThemeAsync();

    /// <summary>
    /// Get app theme
    /// </summary>
    /// <returns>0: auto, 1: light, 2: dark</returns>
    ValueTask<int> GetThemeAsync();

    /// <summary>
    /// Check if the system theme is dark
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> IsDarkThemeOfSystemAsync();

    Task SetCacheAsync<TValue>(string key, TValue value);

    Task<TValue> GetCacheAsync<TValue>(string key, TValue defaultValue);

    Task RemoveCacheAsync(string key);
}