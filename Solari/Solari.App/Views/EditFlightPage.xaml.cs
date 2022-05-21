﻿using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class EditFlightPage : Page
    {
        public EditFlightViewModel ViewModel { get; }

        public EditFlightPage()
        {
            // Injecting flight service
            ViewModel = Ioc.Default.GetService<EditFlightViewModel>();

            // Assigning view model to context, so that we can use data bindings
            DataContext = ViewModel;

            // Initialize view
            InitializeComponent();

            // Assign the XamlRoot element to the dialog services after window
            // has loaded, aka. when XamlRoot is available and not null.
            Loaded += (sender, e) =>
            {
                ViewModel.ErrorDialogService = new ErrorDialogService(XamlRoot);
                ViewModel.InfoDialogService = new InfoDialogService(XamlRoot);
            };
        }
    }
}
