// <copyright file="FlyoutButtonControl.xaml.cs" company="Visual Software Systems Ltd.">Copyright (c) 2022 All rights reserved</copyright>

namespace FlyoutIssue.Shared
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using FlyoutIssue.Shared.ViewModels;
#if WINDOWS_UWP
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
#else
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
#endif

    // The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

    /// <summary>
    /// Code behind the flayout button cotrol.
    /// </summary>
    public sealed partial class FlyoutButtonControl : UserControl
    {

        /// <summary>
        /// Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FlyoutButtonControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Initializes a new instance of the <see cref="FlyoutButtonControl" /> class.
        /// </summary>
        public FlyoutButtonControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the data context cast as the view model interface
        /// </summary>
        public MainViewModel VM
        {
            get { return this.DataContext as MainViewModel; }
        }

        /// <summary>
        /// Gets or sets the text value to display.
        /// </summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }
    }
}
