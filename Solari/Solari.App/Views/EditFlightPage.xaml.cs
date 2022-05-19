using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class EditFlightPage : Page
    {
        public EditFlightViewModel ViewModel { get; }

        public EditFlightPage()
        {
            ViewModel = Ioc.Default.GetService<EditFlightViewModel>();
            InitializeComponent();
        }
    }
}
