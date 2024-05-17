using Android.App;
using Android.Content.PM;

namespace Masa.Blazor.MauiDemo;

[Activity(
    Theme = "@style/MauiDemoMainTheme",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize
        | ConfigChanges.Orientation
        | ConfigChanges.UiMode
        | ConfigChanges.ScreenLayout
        | ConfigChanges.SmallestScreenSize
        | ConfigChanges.Density
)]
public partial class MainActivity : MauiAppCompatActivity
{
    public MainActivity() => Instance = this;

    public partial void SetFullWebView();

    public partial void ClearFullWV(
        Android.Graphics.Color statusColor,
        Android.Graphics.Color navColor
    );
}

