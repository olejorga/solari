using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.App.Contracts.Services
{
    public enum DialogResult
    {
        Primary,
        Secondary,
        Close
    }

    public interface IDialogService
    {
        Task<DialogResult> ShowAsync(string message);
    }
}
