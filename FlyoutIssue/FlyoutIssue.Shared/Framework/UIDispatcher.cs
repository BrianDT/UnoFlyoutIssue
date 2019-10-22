﻿// <copyright file="UIDispatcher.cs" company="Visual Software Systems Ltd.">Copyright (c) 2013 All rights reserved</copyright>

namespace Vssl.Samples.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Vssl.Samples.FrameworkInterfaces;
    using Windows.UI.Core;
    using Windows.UI.Xaml;

    /// <summary>
    /// A UWP platform specific implementation of the UI Dispatcher facade
    /// </summary>
    public class UIDispatcher : IDispatchOnUIThread
    {
        /// <summary>
        /// The UWP platform dispatcher
        /// </summary>
        private CoreDispatcher dispatcher;

        /// <summary>
        /// Initialise the dispatcher
        /// </summary>
        public void Initialize()
        {
            if (Window.Current != null)
            {
                this.dispatcher = Windows.UI.Xaml.Window.Current.Dispatcher;
            }

            DispatchHelper.Initialise(this);
        }

        /// <summary>
        /// Check the dispatcher is initialised and if not initialise it
        /// </summary>
        public void CheckDispatcher()
        {
            if (this.dispatcher == null)
            {
                this.Initialize();
            }
        }

        /// <summary>
        /// Execute an action via the dispatcher
        /// </summary>
        /// <param name="action">The action</param>
        public void Invoke(Action action)
        {
            this.ExecuteNoWait(action);
        }

        /// <summary>
        /// Async Execution of an action via the dispatcher
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>An awaitable task</returns>
        public async Task InvokeAsync(Action action)
        {
            await this.Execute(action);
        }

        /// <summary>
        /// Execute an action via the dispatcher
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="delayms">Any delay before the execution</param>
        /// <param name="priority">The priority</param>
        public void ExecuteNoWait(Action action, int delayms = 0, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            if (delayms > 0)
            {
                Task.Delay(delayms).ConfigureAwait(this.dispatcher.HasThreadAccess).GetAwaiter().GetResult();
            }

            if (this.dispatcher == null || (this.dispatcher.HasThreadAccess && priority == CoreDispatcherPriority.Normal))
            {
                action();
            }
            else
            {
                // Not awated execution will continue before the action has completed
                this.dispatcher.RunAsync(priority, () => action()).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Execute an action via the dispatcher
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="delayms">Any delay before the execution</param>
        /// <param name="priority">The priority</param>
        /// <returns>An awaitable task</returns>
        public async Task Execute(Action action, int delayms = 0, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            if (delayms > 0)
            {
                await Task.Delay(delayms).ConfigureAwait(this.dispatcher.HasThreadAccess);
            }

            if (this.dispatcher == null || (this.dispatcher.HasThreadAccess && priority == CoreDispatcherPriority.Normal))
            {
                action();
            }
            else
            {
                var tcs = new TaskCompletionSource<object>();

                await this.dispatcher.RunAsync(
                    priority,
                    () =>
                    {
                        try
                        {
                            action();
                            tcs.TrySetResult(null);
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }).AsTask().ConfigureAwait(false);
                await tcs.Task.ConfigureAwait(false);
            }
        }
    }
}
