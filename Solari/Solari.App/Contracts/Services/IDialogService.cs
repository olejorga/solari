using System.Threading.Tasks;

namespace Solari.App.Contracts.Services
{
    public enum DialogResult
    {
        Primary,
        Secondary,
        Close
    }

    // See DialogService.cs for abstract implementation.
    public interface IDialogService
    {
        Task<DialogResult> ShowAsync(string message);
    }
}
