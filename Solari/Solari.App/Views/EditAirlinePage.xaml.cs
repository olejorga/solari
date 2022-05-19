using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class EditAirlinePage : Page
    {
        public EditAirlineViewModel ViewModel { get; }

        public EditAirlinePage()
        {
            ViewModel = Ioc.Default.GetService<EditAirlineViewModel>();
            InitializeComponent();
        }
    }
}
