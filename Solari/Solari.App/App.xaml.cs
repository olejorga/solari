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

// To learn more about WinUI3, see: https://docs.microsoft.com/windows/apps/winui/winui3/.
namespace Solari.App
{
    public partial class App : Application
    {
        public static Window MainWindow { get; set; } = new Window() { Title = "AppDisplayName".GetLocalized() };

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
            // TODO WTS: Register your services, viewmodels and pages here
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
            services.AddSingleton<ISampleDataService, SampleDataService>();
            services.AddSingleton<IAirlineService, AirlineService>();

            // Views and ViewModels
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<LandingViewModel>();
            services.AddTransient<LandingPage>();
            services.AddTransient<DeparturesViewModel>();
            services.AddTransient<DeparturesPage>();
            services.AddTransient<ArrivalsViewModel>();
            services.AddTransient<ArrivalsPage>();
            services.AddTransient<FlightViewModel>();
            services.AddTransient<FlightPage>();
            services.AddTransient<FlightDetailViewModel>();
            services.AddTransient<FlightDetailPage>();
            services.AddTransient<AddViewModel>();
            services.AddTransient<AddPage>();
            services.AddTransient<EditViewModel>();
            services.AddTransient<EditPage>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            return services.BuildServiceProvider();
        }
    }
}
