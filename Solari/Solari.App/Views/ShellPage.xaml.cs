using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
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

        public ShellPage(ShellViewModel viewModel, IAirportService airportSerivce, IFlightService flightService)
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
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            KeyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            KeyboardAccelerators.Add(_backKeyboardAccelerator);
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerator = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            var navigationService = Ioc.Default.GetService<INavigationService>();
            var result = navigationService.GoBack();
            args.Handled = result;
        }

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
                        allAirports.Add($"{airport.Name} - {airport.Icao}");
                    }
                }
                catch
                {
                    suitableAirports.Add("Error - can't fetch airports.");
                    sender.ItemsSource = suitableAirports;

                    return;
                }
                
                string[] splitText = sender.Text.ToLower().Split(" ");

                foreach (string airport in allAirports)
                {
                    var found = splitText.All((key) =>
                    {
                        return airport.ToLower().Contains(key);
                    });
                    if (found)
                    {
                        suitableAirports.Add(airport);
                    }
                }

                if (suitableAirports.Count == 0)
                {
                    suitableAirports.Add("No airports found.");
                }

                sender.ItemsSource = suitableAirports;
            }
        }

        private void Search_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            //SuggestionOutput.Text = args.SelectedItem.ToString();
            Session
        }
    }
}
