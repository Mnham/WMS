using Windows.Storage;
using Windows.UI.Xaml;

namespace WMS.Manager.Infrastructure.Helpers;

public static class ThemeHelper
{
    private const string SelectedAppThemeKey = "SelectedAppTheme";
    private static Window CurrentWindow;

    public static ElementTheme ActualTheme => ((FrameworkElement)CurrentWindow.Content).RequestedTheme;

    public static ElementTheme RootTheme
    {
        get => CurrentWindow.Content is FrameworkElement rootElement
            ? rootElement.RequestedTheme
            : ElementTheme.Default;
        set
        {
            if (CurrentWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = value;
            }

            ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
        }
    }

    public static void Initialize()
    {
        CurrentWindow = Window.Current;
        string savedTheme = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString();

        if (savedTheme is not null)
        {
            RootTheme = EnumHelper.GetEnum<ElementTheme>(savedTheme);
        }
    }
}
