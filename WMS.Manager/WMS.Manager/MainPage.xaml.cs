using System;

using Windows.UI.Xaml.Controls;

using WMS.Manager.Settings;

using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NavigationViewSelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs;

namespace WMS.Manager
{
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

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