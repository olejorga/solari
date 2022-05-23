using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.App.Helpers;
using Solari.App.ViewModels;
using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.System;

namespace Solari.App.Views
{
    public sealed partial class ShellPage : Page
    {
        private readonly KeyboardAccelerator _altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
        private readonly KeyboardAccelerator _backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);

        private readonly IAirportService _airportSerivce;

        public ShellViewModel ViewModel { get; }

        public ShellPage(ShellViewModel viewModel, IAirportService airportSerivce)
        {
            _airportSerivce = airportSerivce;
            ViewModel = viewModel;

            InitializeComponent();

            ViewModel.NavigationService.Frame = shellFrame;
            ViewModel.NavigationViewService.Initialize(navigationView);
        }

        private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            KeyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            KeyboardAccelerators.Add(_backKeyboardAccelerator);
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            KeyboardAccelerator keyboardAccelerator = new() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            INavigationService navigationService = Ioc.Default.GetService<INavigationService>();
            bool result = navigationService.GoBack();
            args.Handled = result;
        }

        /// <summary>
        /// Handles the auto suggest airport search in the navigation menu.
        /// </summary>
        private async void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                List<string> allAirports = new();
                List<string> suitableAirports = new();

                try
                {
                    IEnumerable<Airport> dbAirports = await _airportSerivce.GetAirportsAsync();

                    foreach (Airport airport in dbAirports)
                    {
                        allAirports.Add($"{airport.City} {airport.Name} ({airport.Iata}) - {airport.Icao}");
                    }
                }

                // If fetching airports fails.
                catch
                {
                    suitableAirports.Add("Error - can't fetch airports.");
                    sender.ItemsSource = suitableAirports;

                    return;
                }

                // Split search query into search phrases.
                string[] splitText = sender.Text.ToLower().Split(" ");

                // Match each airport with all phrases.
                foreach (string airport in allAirports)
                {
                    bool found = splitText.All((key) =>
                    {
                        return airport.ToLower().Contains(key);
                    });

                    // If a match is found, add airport to search suggestions.
                    if (found)
                    {
                        suitableAirports.Add(airport);
                    }
                }

                // If there are no matching airports.
                if (suitableAirports.Count == 0)
                {
                    suitableAirports.Add("No airports found.");
                }


                sender.ItemsSource = suitableAirports;
            }
        }

        /// <summary>
        /// Handles when the user selects a suggested airport from the auto suggest box.
        /// </summary>
        private async void Search_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync("SelectedAirportIcao", args.SelectedItem.ToString()[^4..]);

            // Navigate user to departures view after search.
            _ = ViewModel.NavigationService.NavigateTo("Solari.App.ViewModels.LandingViewModel");
            _ = ViewModel.NavigationService.NavigateTo("Solari.App.ViewModels.DeparturesViewModel");
        }
    }
}
