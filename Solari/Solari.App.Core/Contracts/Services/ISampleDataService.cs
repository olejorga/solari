using System.Collections.Generic;
using System.Threading.Tasks;

using Solari.App.Core.Models;

namespace Solari.App.Core.Contracts.Services
{
    // Remove this class once your pages/features are using your data.
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();

        Task<IEnumerable<SampleOrder>> GetGridDataAsync();

        Task<IEnumerable<SampleOrder>> GetContentGridDataAsync();
    }
}
