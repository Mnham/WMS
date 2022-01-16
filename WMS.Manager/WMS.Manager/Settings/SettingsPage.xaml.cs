using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WMS.Manager.Infrastructure.Helpers;

namespace WMS.Manager.Settings
{
    public sealed partial class SettingsPage : Page
    {
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
