using Microsoft.UI.Xaml;
using Solari.App.Contracts.Services;
using Solari.App.Services;
using System.Threading.Tasks;

namespace Solari.App.ViewModels
{
    /// <summary>
    /// A tailored dialog service for error messages. 
    /// </summary>
    public class ErrorDialogService : DialogService
    {
        public ErrorDialogService(XamlRoot xamlRoot) : base(xamlRoot)
        { }

        public override async Task<DialogResult> ShowAsync(string message)
        {
            return await CreateDialogAsync("Error", message, null, null, "Understood");
        }
    }
}
