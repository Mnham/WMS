using Windows.Storage;
using Windows.UI.Xaml;

namespace WMS.Manager.Infrastructure.Helpers
{
    /// <summary>
    /// ������������ �������� ��� ������������ ���� ���������� ����������.
    /// </summary>
    public static class ThemeHelper

    {
        /// <summary>
        /// ��� ���������� �������� ��� �������� ��������� ����.
        /// </summary>
        private const string SELECTED_APP_THEME_KEY = "SelectedAppTheme";

        /// <summary>
        /// ������� ���� ����������.
        /// </summary>
        private static Window _currentWindow;

        /// <summary>
        /// ������� ����.
        /// </summary>
        public static ElementTheme ActualTheme => ((FrameworkElement)_currentWindow.Content).RequestedTheme;

        /// <summary>
        /// ������� ��������� ����.
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
        /// �������������� �����. 
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

