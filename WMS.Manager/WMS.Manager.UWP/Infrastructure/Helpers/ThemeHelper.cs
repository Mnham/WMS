using Windows.Storage;
using Windows.UI.Xaml;

using WMS.Manager.Infrastructure.Helpers;

namespace WMS.Manager.UWP.Infrastructure.Helpers
{
    /// <summary>
    /// Представляет помощник для переключения темы оформления приложения.
    /// </summary>
    public static class ThemeHelper

    {
        /// <summary>
        /// Имя локального свойства для хранения выбранной темы.
        /// </summary>
        private const string SELECTED_APP_THEME_KEY = "SelectedAppTheme";

        /// <summary>
        /// Текущее окно приложения.
        /// </summary>
        private static Window _currentWindow;

        /// <summary>
        /// Текущая тема.
        /// </summary>
        public static ElementTheme ActualTheme => ((FrameworkElement)_currentWindow.Content).RequestedTheme;

        /// <summary>
        /// Текущая системная тема.
        /// </summary>
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

        /// <summary>
        /// Инициализирует класс.
        /// </summary>
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