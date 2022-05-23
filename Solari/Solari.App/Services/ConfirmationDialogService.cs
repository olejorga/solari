using Microsoft.UI.Xaml;
using Solari.App.Contracts.Services;
using Solari.App.Services;
using System.Threading.Tasks;

namespace Solari.App.ViewModels
{
    /// <summary>
    /// A tailored dialog service for messages needing confirmation. 
    /// </summary>
    public class ConfirmationDialogService : DialogService
    {
        public ConfirmationDialogService(XamlRoot xamlRoot) : base(xamlRoot)
        {
        }

        public override async Task<DialogResult> ShowAsync(string message)
        {
            return await CreateDialogAsync("Info", message, "Yes", "No", null);
        }
    }
}
