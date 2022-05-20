using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Solari.App.Contracts.Services;
using Solari.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.App.ViewModels
{
    /// <summary>
    /// A tailored dialog service for info messages. 
    /// </summary>
    public class InfoDialogService : DialogService
    {
        public InfoDialogService(XamlRoot xamlRoot) : base(xamlRoot)
        {
        }

        public override async Task<DialogResult> ShowAsync(string message)
        {
            return await CreateDialogAsync("Info", message, null, null, "Close");
        }
    }
}
