using Windows.Storage;
using Windows.UI.Xaml;

namespace WMS.Manager.Infrastructure.Helpers
{
    public static class ThemeHelper
    {
        private const string SELECTED_APP_THEME_KEY = "SelectedAppTheme";
        private static Window _currentWindow;

        public static ElementTheme ActualTheme => ((FrameworkElement)_currentWindow.Content).RequestedTheme;

        public static ElementTheme RootTheme
        {
            get => _currentWindow.Content is FrameworkElement rootElement
                ? rootElement.RequestedTheme
                : ElementTheme.Default;
            set
            {
                if (_currentWindow.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = value;
                }

                ApplicationData.Current.LocalSettings.Values[SELECTED_APP_THEME_KEY] = value.ToString();
            }
        }

        public static void Initialize()
        {
            _currentWindow = Window.Current;
            string savedTheme = ApplicationData.Current.LocalSettings.Values[SELECTED_APP_THEME_KEY]?.ToString();

            if (savedTheme is not null)
            {
                RootTheme = EnumHelper.GetEnum<ElementTheme>(savedTheme);
            }
        }
    }

}

