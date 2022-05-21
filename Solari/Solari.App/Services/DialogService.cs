using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Solari.App.Contracts.Services;
using System;
using System.Threading.Tasks;

namespace Solari.App.Services
{
    /// <summary>
    /// A abstract dialog service for delivering pop-up messages
    /// to the user. This is made in such a way, so that it 
    /// abstracts away the details associated with the view, 
    /// since it is intended to be called in the view model.
    /// 
    /// The idea behind this, is that it is more in-line with 
    /// the MVVM pattern, because you can mock the service so 
    /// that it is easier to unit test.
    /// </summary>
    public abstract class DialogService : IDialogService
    {
        private readonly XamlRoot _XamlRoot;

        public DialogService(XamlRoot xamlRoot)
        {
            _XamlRoot = xamlRoot;
        }

        /// <summary>
        /// A abstraction of the process of building dialogs. It
        /// helps to hide away the assignment of props, that are 
        /// common to all dialogs. E.g. XamlRoot.
        /// 
        /// All values set to "null" will be hidden in the view.
        /// </summary>
        /// <param name="title">The title text of the dialog modal.</param>
        /// <param name="message">The message text of the dialog modal.</param>
        /// <param name="primaryButtonText">The primary button text.</param>
        /// <param name="secondaryButtonText">The secondary button text.</param>
        /// <param name="closeButtonText">The close button text.</param>
        /// <returns>One of three predefined states.</returns>
        protected async Task<DialogResult> CreateDialogAsync(string title, string message, string primaryButtonText = null, string secondaryButtonText = null, string closeButtonText = null)
        {
            ContentDialog dialog = new()
            {
                XamlRoot = _XamlRoot,
                DefaultButton = ContentDialogButton.Primary,
                Title = title,
                Content = message,
                PrimaryButtonText = primaryButtonText,
                SecondaryButtonText = secondaryButtonText,
                CloseButtonText = closeButtonText
            };

            ContentDialogResult result = await dialog.ShowAsync();

            return result switch
            {
                ContentDialogResult.Primary => DialogResult.Primary,
                ContentDialogResult.Secondary => DialogResult.Secondary,
                ContentDialogResult.None => DialogResult.Close,
                _ => DialogResult.Close,
            };
        }

        /// <summary>
        /// Shows a dialog with the supplied message.
        /// 
        /// Intended to be overwritten in tailored dialog 
        /// service implementations. This way you can 
        /// populate the message dynamically, but the 
        /// overall design stays the same in that 
        /// certain type of dialog.
        /// </summary>
        /// <param name="message">The message text</param>
        /// <returns>One of three predefined states.</returns>
        public abstract Task<DialogResult> ShowAsync(string message);
    }
}
