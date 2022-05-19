using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class AddAirlinePage : Page
    {
        public AddAirlineViewModel ViewModel { get; }

        public AddAirlinePage()
        {
            ViewModel = Ioc.Default.GetService<AddAirlineViewModel>();
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
