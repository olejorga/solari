using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using Solari.App.Contracts.Services;
using Solari.App.Helpers;
using Solari.App.ViewModels;
using Solari.Data.Access.Models;
using System;
using System.Diagnostics;
using Windows.Storage;

namespace Solari.App.Views
{
    // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on DataGridPage.xaml.
    // For more details see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid
    public sealed partial class DeparturesPage : Page
    {
        public DeparturesViewModel ViewModel { get; }

        private readonly INavigationService _navigationService;

        public DeparturesPage()
        {
            ViewModel = Ioc.Default.GetService<DeparturesViewModel>();
            _navigationService = Ioc.Default.GetService<INavigationService>();

            InitializeComponent();
        }

        private async void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Flight flight = (Flight) DataGrid.SelectedItem;

            await ApplicationData.Current.LocalSettings.SaveAsync("SelectedFlightNumber", flight.FlightNumber);

            _navigationService.NavigateTo("Solari.App.ViewModels.FlightViewModel");
        }
    }
}
