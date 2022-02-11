using MahApps.Metro.Controls;

using MaterialDesignThemes.Wpf;

using System;
using System.Windows;
using System.Windows.Controls;

namespace WMS.Manager.WPF
{
    public partial class MainWindow : MetroWindow
    {
        private static Snackbar Snackbar;

        public MainWindow()
        {
            InitializeComponent();
            Snackbar = MainSnackbar;
        }

        private void MenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem.Tag is string selectedItemTag)
            {
                Type pageType = Type.GetType(selectedItemTag);
                object page = Activator.CreateInstance(pageType);
                ContentFrame.Content = page;
            }
        }

        public static void EnqueueMessage(object content) => Snackbar.MessageQueue.Enqueue(content);
    }
}