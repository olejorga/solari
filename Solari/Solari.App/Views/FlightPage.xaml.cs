using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;
using System;

namespace Solari.App.Views
{
    public sealed partial class FlightPage : Page
    {
        public FlightViewModel ViewModel { get; }

        public FlightPage()
        {
            // Injecting flight view model
            ViewModel = Ioc.Default.GetService<FlightViewModel>();

            // Assigning view model to context, so that we can use data bindings
            DataContext = ViewModel;

            // Initialize view
            InitializeComponent();

            // Assign the XamlRoot element to the dialog services after window
            // has loaded, aka. when XamlRoot is available and not null.
            Loaded += (sender, e) =>
                ViewModel.ErrorDialogService = new ErrorDialogService(XamlRoot);
        }
    }
}
