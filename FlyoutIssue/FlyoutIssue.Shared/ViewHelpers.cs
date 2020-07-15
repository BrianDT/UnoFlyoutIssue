// <copyright file="MainPage.xaml.cs" company="Visual Software Systems Ltd.">Copyright (c) 2020 All rights reserved</copyright>

namespace FlyoutIssue.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Uno.Extensions.ValueType;
    using Windows.UI.Xaml;

    /// <summary>
    /// Attached properties
    /// </summary>
    public static class ViewHelpers
    {
        /// <summary>
        /// Gets the maximum flyout text width
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns></returns>
        public static double GetMaxFlyoutTextWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MaxFlyoutTextWidthProperty);
        }

        /// <summary>
        /// Sets the maximum flyout text width
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The new value</param>
        public static void SetMaxFlyoutTextWidth(DependencyObject obj, double value)
        {
            obj.SetValue(MaxFlyoutTextWidthProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for MaxFlyoutTextWidth.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MaxFlyoutTextWidthProperty =
            DependencyProperty.RegisterAttached("MaxFlyoutTextWidth", typeof(double), typeof(ViewHelpers), new PropertyMetadata(0.0D));
    }
}
