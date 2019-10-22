// <copyright file="MainViewModel.cs" company="Visual Software Systems Ltd.">Copyright (c) 2019 All rights reserved</copyright>

namespace FlyoutIssue.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FlyoutIssue.Shared.Services;
    using Vssl.Samples.Framework;
    using Vssl.Samples.ViewModels;

    /// <summary>
    /// The view model for the fly-out issue sample
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Platform specific dialog display
        /// </summary>
        private IDialogService dialogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">Platform specific dialog display</param>
        public MainViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.DoStuffCommand = new DelegateCommandAsync(this.DoStuff, (p) => { return true; });
        }

        /// <summary>
        /// Gets or sets a value indicating whether the fly-out command has been activated
        /// </summary>
        public bool Ping { get; set; }

        /// <summary>
        /// Gets the command activated by the fly-out
        /// </summary>
        public ICommand DoStuffCommand { get; private set; }

        /// <summary>
        /// Executes the command activated by the fly-out
        /// </summary>
        /// <param name="parameter">An optional parameter</param>
        /// <returns>An awaitable task</returns>
        private async Task DoStuff(object parameter)
        {
            this.Ping = true;
            this.OnPropertyChanged("Ping");
            await this.dialogService.ShowMessage("You tapped me");
        }
    }
}
