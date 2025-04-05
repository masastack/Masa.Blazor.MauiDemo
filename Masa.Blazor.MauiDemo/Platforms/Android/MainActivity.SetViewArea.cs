using Android.Views;
using Color = Android.Graphics.Color;

namespace Masa.Blazor.MauiDemo;

public partial class MainActivity
{
    /// <summary>
    /// 静态化
    /// </summary>
    public static MainActivity? Instance { get; private set; }

    #region Status
    private readonly WindowManagerFlags statusFlag = WindowManagerFlags.TranslucentStatus;
    /// <summary>
    /// 设置状态栏透明
    /// </summary>
    public void SetStatusTransparent() => SetPart(statusFlag, Color.Transparent);

    public void ClearStatus(Color color) => ClearPart(statusFlag, color);
    #endregion

    #region Navigation
    private readonly WindowManagerFlags navFlag = WindowManagerFlags.TranslucentNavigation;
    /// <summary>
    /// 设置导航栏透明
    /// </summary>
    public void SetNavTransparent() => SetPart(navFlag, Color.Transparent);

    public void ClearNav(Color color) => ClearPart(navFlag, color);
    #endregion

    #region Full
    /// <summary>
    /// 设置全屏
    /// </summary>
    public partial void SetFullWebView()
    {
        SetStatusTransparent();
        SetNavTransparent();
    }
    /// <summary>
    /// 取消全屏
    /// </summary>
    /// <param name="statusColor">状态栏颜色</param>
    /// <param name="navColor">导航栏颜色</param>
    public partial void ClearFullWV(Color statusColor, Color navColor)
    {
        ClearStatus(statusColor);
        ClearNav(navColor);
    }
    #endregion

    #region Core
    public void SetPart(WindowManagerFlags flag, Color color)
    {
        if (Window is null)
        {
            return;
        }

        Window.SetFlags(flag, flag);
        SetColor(flag, color);
    }

    private void SetColor(WindowManagerFlags flag, Color color)
    {
        if (Window is null)
        {
            return;
        }

        if (flag == statusFlag)
        {
            Window.SetStatusBarColor(color);
        }
        else if (flag == navFlag)
        {
            Window.SetNavigationBarColor(color);
        }
        else { }
    }

    public void ClearPart(WindowManagerFlags flag, Color color)
    {
        if (Window is null)
        {
            return;
        }

        Window.ClearFlags(flag);
        SetColor(flag, color);
    }
    #endregion
}
