using System;

using Windows.UI.Xaml.Controls;

using WMS.Manager.UWP.Settings;

using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NavigationViewSelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs;

namespace WMS.Manager.UWP
{
    /// <summary>
    /// Представляет основную страницу.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="MainPage"/>.
        /// </summary>
        public MainPage() => InitializeComponent();

        /// <summary>
        /// Обрабатывает изменение выбранного эелемента <see cref="NavigationView"/>.
        /// </summary>
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigationViewItem selectedItem = (NavigationViewItem)args.SelectedItem;
                if (selectedItem.Tag is string selectedItemTag)
                {
                    sender.Header = selectedItem.Content;
                    Type pageType = Type.GetType(selectedItemTag);
                    contentFrame.Navigate(pageType);
                }
            }
        }
    }
}