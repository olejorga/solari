using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on DataGridPage.xaml.
    // For more details see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid
    public sealed partial class ArrivalsPage : Page
    {
        public ArrivalsViewModel ViewModel { get; }

        public ArrivalsPage()
        {
            ViewModel = Ioc.Default.GetService<ArrivalsViewModel>();
            InitializeComponent();
        }
    }
}
