using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

using System;
using System.Runtime.InteropServices;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WMS.Manager.GrpcClient.Clients;
using WMS.Manager.UWP.Infrastructure.Helpers;
using WMS.Manager.UWP.Infrastructure.Services;
using WMS.Manager.UWP.Nomenclature;
using WMS.Manager.UWP.NomenclatureType;

namespace WMS.Manager.UWP
{
    /// <summary>
    /// Представляет точку входа приложения.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="App"/>.
        /// </summary>
        public App()
        {
            ConfigureLogger();
            //NativeMethods.AllocConsole();
            Services = ConfigureServices();
            ConfigureWmsGrpcClient();
            InitializeComponent();
        }

        /// <summary>
        /// Текущий экземпляр <see cref="App"/>.
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Экземпляр служб приложения.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Конфигурирует логгер.
        /// </summary>
        public static void ConfigureLogger() =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

        /// <summary>
        /// Конфигурирует запуск приложения.
        /// </summary>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            if (Window.Current.Content is not Frame rootFrame)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();
                ThemeHelper.Initialize();
            }
        }

        /// <summary>
        /// Конфигурирует службы приложения.
        /// </summary>
        private static IServiceProvider ConfigureServices() =>
            new ServiceCollection()
                .AddSingleton<DialogService>()
                .AddSingleton<WmsGrpcClient>()
                .AddTransient<NomenclaturePageViewModel>()
                .AddTransient<NomenclatureTypePageViewModel>()
                .BuildServiceProvider();

        /// <summary>
        /// Конфигурирует WmsGrpc-клиент.
        /// </summary>
        private void ConfigureWmsGrpcClient()
        {
            WmsGrpcClient grpcClient = Services.GetService<WmsGrpcClient>();
            DialogService serviceDialog = Services.GetService<DialogService>();

            grpcClient.SetExceptionHandler(async ex => await serviceDialog.ShowExceptionDialogAsync(ex));
        }
    }

    /// <summary>
    /// Представляет вызовы нативных методов.
    /// </summary>
    internal static class NativeMethods
    {
        #region Public Methods

        /// <summary>
        /// Выделяет новую консоль для вызывающего процесса.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        #endregion Public Methods
    }
}