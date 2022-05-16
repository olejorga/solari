using System;

namespace Solari.App.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}
