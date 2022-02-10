using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WMS.Manager.Infrastructure.Helpers;
using WMS.Manager.UWP.Infrastructure.Helpers;

namespace WMS.Manager.UWP.Settings
{
    /// <summary>
    /// Представляет настройки приложения.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureTypePageViewModel"/>.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
            switch (ThemeHelper.ActualTheme)
            {
                case ElementTheme.Default:
                    DefaultTheme.IsChecked = true;
                    break;

                case ElementTheme.Light:
                    LightTheme.IsChecked = true;
                    break;

                case ElementTheme.Dark:
                    DarkTheme.IsChecked = true;
                    break;
            }
        }

        /// <summary>
        /// Обрабатывает переключение темы оформления приложения.
        /// </summary>
        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            string selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
            if (selectedTheme is not null)
            {
                ThemeHelper.RootTheme = EnumHelper.GetEnum<ElementTheme>(selectedTheme);
            }
        }
    }
}