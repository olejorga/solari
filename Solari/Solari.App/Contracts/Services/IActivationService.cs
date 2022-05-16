using System.Threading.Tasks;

namespace Solari.App.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}
