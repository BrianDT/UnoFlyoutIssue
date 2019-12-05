// <copyright file="MainPage.xaml.cs" company="Visual Software Systems Ltd.">Copyright (c) 2019 All rights reserved</copyright>

namespace FlyoutIssue
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using FlyoutIssue.Shared.Services;
    using FlyoutIssue.Shared.ViewModels;
    using Vssl.Samples.Framework;
    using Vssl.Samples.FrameworkInterfaces;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    // The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
    // <copyright file="MainPage.xaml.cs" company="Visual Software Systems Ltd.">Copyright (c) 2019 All rights reserved</copyright>

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// The dispatcher facade
        /// </summary>
        private IDispatchOnUIThread dispatcher;

        /// <summary>
        /// Indicates whether the fly-out will be hidden manually on button press
        /// </summary>
        private bool doManualFlyoutHide;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            this.dispatcher = new UIDispatcher();
            this.dispatcher.Initialize();

            // This would normally be injected but for brevity
            this.DataContext = new MainViewModel(new DialogService());
            this.VM.PropertyChanged += this.OnViewModelPropertyChanged;
        }

        /// <summary>
        /// Gets the data context cast as the view model interface
        /// </summary>
        public MainViewModel VM
        {
            get { return this.DataContext as MainViewModel; }
        }

        /// <summary>
        /// The event handler for tap on the primary shape
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void Open_Flyout(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        /// <summary>
        /// The event handler for tap on the enable had dismiss button
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void Enable_Manual_Dismiss(object sender, RoutedEventArgs e)
        {
            this.doManualFlyoutHide = true;
            this.VM.EnableManualDismiss = false;
        }

        /// <summary>
        /// An event handler for view model property change
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Ping":
                    if (this.VM.Ping)
                    {
                        this.VM.Ping = false;
                        if (this.doManualFlyoutHide)
                        {
                            this.flyout.Hide();
                        }
                    }

                    break;
            }
        }
    }
}
