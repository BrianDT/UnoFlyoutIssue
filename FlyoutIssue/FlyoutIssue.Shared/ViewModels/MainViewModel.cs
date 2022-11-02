// <copyright file="MainViewModel.cs" company="Visual Software Systems Ltd.">Copyright (c) 2019, 2020 All rights reserved</copyright>

namespace FlyoutIssue.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FlyoutIssue.Shared.Services;
    using Vssl.Samples.Framework;
    using Vssl.Samples.ViewModels;

    /// <summary>
    /// The view model for the fly-out issue sample
    /// </summary>
    [Bindable(bindable: true)]
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Platform specific dialog display
        /// </summary>
        private IDialogService dialogService;

        /// <summary>
        /// A value indicating whether the enable manual dismiss button is enabled
        /// </summary>
        private bool enableManualDismiss;

        private double flyoutTextReduction;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="dialogService">Platform specific dialog display</param>
        public MainViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.enableManualDismiss = true;
            this.DoStuffCommand = new DelegateCommandAsync(this.DoStuff, (p) => { return true; });
            this.MaxFlyoutTextWidth = Double.PositiveInfinity;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the fly-out command has been activated
        /// </summary>
        public bool Ping { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the enable manual dismiss button is enabled
        /// </summary>
        public bool EnableManualDismiss
        {
            get
            {
                return this.enableManualDismiss;
            }

            set
            {
                if (value != this.enableManualDismiss)
                {
                    this.enableManualDismiss = value;
                    this.OnPropertyChanged("EnableManualDismiss");
                }
            }
        }

        /// <summary>
        /// Gets the command activated by the fly-out
        /// </summary>
        public ICommand DoStuffCommand { get; private set; }

        /// <summary>
        /// Gets the max width allowed fro the flyout text
        /// </summary>
        public double MaxFlyoutTextWidth { get; private set; }

        /// <summary>
        /// Used to monitor size change
        /// </summary>
        /// <param name="width">The new width</param>
        /// <param name="height">The new height</param>
        public void PageSizeChanged(double width, double height)
        {
            if (width > 0 && height > 0)
            {
                var newValue = width - this.flyoutTextReduction;
                if (newValue != this.MaxFlyoutTextWidth)
                {
                    this.MaxFlyoutTextWidth = newValue;
                    this.OnPropertyChanged("MaxFlyoutTextWidth");
                }
            }
        }

        /// <summary>
        /// Sets the display secific text reduction to be applied to the page width
        /// </summary>
        /// <param name="flyoutTextReduction">The text reduction</param>
        public void SetFlyoutTextReduction(double flyoutTextReduction)
        {
            this.flyoutTextReduction = flyoutTextReduction;
        }

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
