using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Solari.App.Services;
using Solari.App.ViewModels;
using System;

namespace Solari.App.Views
{
    public sealed partial class AddAirlinePage : Page
    {
        public AddAirlineViewModel ViewModel { get; }

        public AddAirlinePage()
        {
            ViewModel = Ioc.Default.GetService<AddAirlineViewModel>();
            DataContext = ViewModel;
            InitializeComponent();

            Loaded += (sender, e) =>
            {
                ViewModel.ErrorDialogService = new ErrorDialogService(XamlRoot);
                ViewModel.InfoDialogService = new InfoDialogService(XamlRoot);
            };
        }
    }
}
