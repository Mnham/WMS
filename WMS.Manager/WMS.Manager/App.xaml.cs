using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

using System;
using System.Runtime.InteropServices;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Globalization;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WMS.Manager.GrpcClient.Clients;
using WMS.Manager.Infrastructure.Helpers;
using WMS.Manager.Infrastructure.Services;
using WMS.Manager.Nomenclature;

namespace WMS.Manager
{
    public sealed partial class App : Application
    {
        public static new App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices() =>
            new ServiceCollection()
            .AddSingleton<DialogService>()
            .AddSingleton<WmsGrpcClient>()
            .AddTransient<NomenclaturePageViewModel>()
            .BuildServiceProvider();

        public static void ConfigureLogger() =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

        public App()
        {
            ConfigureLogger();
            //NativeMethods.AllocConsole();

            string[] langs = new string[]
            {
                "ar-AE", // 0
                "de-DE", // 1
                "en-US", // 2
                "es-ES", // 3
                "fr-FR", // 4
                "it-IT", // 5
                "ja-JP", // 6
                "ko-KR", // 7
                "pt-PT", // 8
                "ru-RU", // 9
                "zh-CN", // 10
            };

            ApplicationLanguages.PrimaryLanguageOverride = langs[9];
            Services = ConfigureServices();
            ConfigureWmsGrpcClient();
            InitializeComponent();
        }

        private void ConfigureWmsGrpcClient()
        {
            WmsGrpcClient grpcClient = Services.GetService<WmsGrpcClient>();
            DialogService serviceDialog = Services.GetService<DialogService>();

            grpcClient.ExceptionHandler = async ex => await serviceDialog.ShowExceptionDialogAsync(ex);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            if (Window.Current.Content is not Frame rootFrame)
            {
                rootFrame = new Frame();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Загрузить состояние из ранее приостановленного приложения
                }

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
    }

    internal static class NativeMethods
    {
        #region Public Methods

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        #endregion Public Methods
    }
}