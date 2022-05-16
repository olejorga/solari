﻿using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class FlightPage : Page
    {
        public FlightViewModel ViewModel { get; }

        public FlightPage()
        {
            ViewModel = Ioc.Default.GetService<FlightViewModel>();
            InitializeComponent();
        }
    }
}
