using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Solari.App.Core.Models;

namespace Solari.App.Views
{
    public sealed partial class EditDetailControl : UserControl
    {
        public SampleOrder ListDetailsMenuItem
        {
            get { return GetValue(ListDetailsMenuItemProperty) as SampleOrder; }
            set { SetValue(ListDetailsMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(SampleOrder), typeof(EditDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

        public EditDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as EditDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
