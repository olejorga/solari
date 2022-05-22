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
            var activationService = Ioc.Default.GetService<IActivationService>();
            await activationService.ActivateAsync(args);
        }

        private System.IServiceProvider ConfigureServices()
        {
            // TODO WTS: Register your services, view models and pages here
            var services = new ServiceCollection();

            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<IAirlineService, AirlineService>();
            services.AddSingleton<IAirportService, AirportService>();
            services.AddSingleton<IFlightService, FlightService>();

            // Views and ViewModels
            // Navigation
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();

            // Start page
            services.AddTransient<LandingViewModel>();
            services.AddTransient<LandingPage>();

            // Airport departures page
            services.AddTransient<DeparturesViewModel>();
            services.AddTransient<DeparturesPage>();

            // Airport arrivals page
            services.AddTransient<ArrivalsViewModel>();
            services.AddTransient<ArrivalsPage>();

            // Flight details page
            services.AddTransient<FlightViewModel>();
            services.AddTransient<FlightPage>();

            // Add data pages
            services.AddTransient<AddAirlineViewModel>();
            services.AddTransient<AddAirlinePage>();
            services.AddTransient<AddAirportViewModel>();
            services.AddTransient<AddAirportPage>();
            services.AddTransient<AddFlightViewModel>();
            services.AddTransient<AddFlightPage>();

            // Edit (and remove) data pages
            services.AddTransient<EditAirlineViewModel>();
            services.AddTransient<EditAirlinePage>();
            services.AddTransient<EditAirportViewModel>();
            services.AddTransient<EditAirportPage>();
            services.AddTransient<EditFlightViewModel>();
            services.AddTransient<EditFlightPage>();

            // Application settings page
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();

            return services.BuildServiceProvider();
        }
    }
}
