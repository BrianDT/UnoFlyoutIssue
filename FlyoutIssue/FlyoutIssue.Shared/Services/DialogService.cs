// <copyright file="DialogService.cs" company="Visual Software Systems Ltd.">Copyright (c) 2019 All rights reserved</copyright>

namespace FlyoutIssue.Shared.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Vssl.Samples.Framework;

    /// <summary>
    /// Platform specific dialog display
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Show a simple pop up message
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <returns>An awaitable task</returns>
        public async Task ShowMessage(string message)
        {
            await DispatchHelper.Dispatcher.InvokeAsync(async () =>
            {
                var dialog = new Windows.UI.Popups.MessageDialog(message);
#if NET6_0 && WINDOWS
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(AppStateHelper.GetMainWindow());
                WinRT.Interop.InitializeWithWindow.Initialize(dialog, hwnd);
#endif
                await dialog.ShowAsync();
            });
        }
    }
}
