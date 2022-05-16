using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Solari.App.Core.Models;

namespace Solari.App.Views
{
    public sealed partial class AddDetailControl : UserControl
    {
        public SampleOrder ListDetailsMenuItem
        {
            get { return GetValue(ListDetailsMenuItemProperty) as SampleOrder; }
            set { SetValue(ListDetailsMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(SampleOrder), typeof(AddDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

        public AddDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as AddDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
