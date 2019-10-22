// <copyright file="IDialogService.cs" company="Visual Software Systems Ltd.">Copyright (c) 2019 All rights reserved</copyright>

namespace FlyoutIssue.Shared.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Platform specific dialog display
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Show a simple pop up message
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <returns>An awaitable task</returns>
        Task ShowMessage(string message);
   }
}
