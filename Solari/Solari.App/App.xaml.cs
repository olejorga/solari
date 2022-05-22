using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

using Solari.App.Activation;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.App.Core.Services;
using Solari.App.Helpers;
using Solari.App.Services;
using Solari.App.ViewModels;
using Solari.App.Views;

namespace Solari.App
{
    public partial class App : Application
    {
        public static Window MainWindow { get; set; } = new Window() { Title = "AppDisplayName".GetLocalized() };

        public struct Config
        {
            public static int SelectedAirportIcao { get; set; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += App_UnhandledException;
            Ioc.Default.ConfigureServices(ConfigureServices());
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.unhandledexceptioneventargs
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            IActivationService activationService = Ioc.Default.GetService<IActivationService>();
            await activationService.ActivateAsync(args);
        }

        private static System.IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new();

            // Default Activation Handler
            _ = services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            _ = services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            _ = services.AddTransient<INavigationViewService, NavigationViewService>();

            _ = services.AddSingleton<IActivationService, ActivationService>();
            _ = services.AddSingleton<IPageService, PageService>();
            _ = services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            _ = services.AddSingleton<IAirlineService, AirlineService>();
            _ = services.AddSingleton<IAirportService, AirportService>();
            _ = services.AddSingleton<IFlightService, FlightService>();

            // Views and ViewModels
            // Navigation
            _ = services.AddTransient<ShellPage>();
            _ = services.AddTransient<ShellViewModel>();

            // Start page
            _ = services.AddTransient<LandingViewModel>();
            _ = services.AddTransient<LandingPage>();

            // Airport departures page
            _ = services.AddTransient<DeparturesViewModel>();
            _ = services.AddTransient<DeparturesPage>();

            // Airport arrivals page
            _ = services.AddTransient<ArrivalsViewModel>();
            _ = services.AddTransient<ArrivalsPage>();

            // Flight details page
            _ = services.AddTransient<FlightViewModel>();
            _ = services.AddTransient<FlightPage>();

            // Add data pages
            _ = services.AddTransient<AddAirlineViewModel>();
            _ = services.AddTransient<AddAirlinePage>();
            _ = services.AddTransient<AddAirportViewModel>();
            _ = services.AddTransient<AddAirportPage>();
            _ = services.AddTransient<AddFlightViewModel>();
            _ = services.AddTransient<AddFlightPage>();

            // Edit (and remove) data pages
            _ = services.AddTransient<EditAirlineViewModel>();
            _ = services.AddTransient<EditAirlinePage>();
            _ = services.AddTransient<EditAirportViewModel>();
            _ = services.AddTransient<EditAirportPage>();
            _ = services.AddTransient<EditFlightViewModel>();
            _ = services.AddTransient<EditFlightPage>();

            // Application settings page
            _ = services.AddTransient<SettingsViewModel>();
            _ = services.AddTransient<SettingsPage>();

            return services.BuildServiceProvider();
        }
    }
}
