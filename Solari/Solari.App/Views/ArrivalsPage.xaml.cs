﻿using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using Solari.App.Helpers;
using Solari.App.ViewModels;
using Solari.Data.Access.Models;
using Windows.Storage;

namespace Solari.App.Views
{
    public sealed partial class ArrivalsPage : Page
    {
        public ArrivalsViewModel ViewModel { get; }

        public ArrivalsPage()
        {
            // Injecting departures view model
            ViewModel = Ioc.Default.GetService<ArrivalsViewModel>();

            // Initialize view
            InitializeComponent();

            // Assign the XamlRoot element to the dialog services after window
            // has loaded, aka. when XamlRoot is available and not null.
            Loaded += (sender, e) =>
                ViewModel.ErrorDialogService = new ErrorDialogService(XamlRoot);

            // Start the table clock.
            Clock watch = new();
            watch.Run(time => ClockDisplay.Text = time);
        }

        /// <summary>
        /// Handles when the user clicks on a flight row in the traffic table.
        /// </summary>
        private async void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Flight flight = (Flight)DataGrid.SelectedItem;

            await ApplicationData.Current.LocalSettings.SaveAsync("SelectedFlightNumber", flight.FlightNumber);

            _ = ViewModel.NavigationService.NavigateTo("Solari.App.ViewModels.FlightViewModel");
        }
    }
}
