// -----------------------------------------------------------------------
// <copyright file="DispatcherExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace BusyIndicators
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    using JetBrains.Annotations;

    /// <summary>
    ///     Dispatcher Extensions
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dependencyObject
        ///     or
        ///     action
        ///     or
        ///     dispatcher
        /// </exception>
        public static void Dispatch([NotNull] this DependencyObject dependencyObject, [NotNull] Action action)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var dispatcher = dependencyObject.Dispatcher;
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(action);
            }
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <typeparam name="TDependencyObject">The type of the dependency object.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dependencyObject
        ///     or
        ///     action
        ///     or
        ///     dispatcher
        /// </exception>
        public static void Dispatch<TDependencyObject>(
            [NotNull] this TDependencyObject dependencyObject,
            [NotNull] Action<TDependencyObject> action)
            where TDependencyObject : DependencyObject
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var dispatcher = dependencyObject.Dispatcher;
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (dispatcher.CheckAccess())
            {
                action(dependencyObject);
            }
            else
            {
                dispatcher.BeginInvoke(new Action(() => action(dependencyObject)));
            }
        }

        /// <summary>
        ///     Dispatches the specified dispatcher priority.
        /// </summary>
        /// <typeparam name="TDependencyObject">The type of the dependency object.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dispatcherPriority">The dispatcher priority.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dependencyObject
        ///     or
        ///     action
        ///     or
        ///     dispatcher
        /// </exception>
        public static void Dispatch<TDependencyObject>(
            [NotNull] this TDependencyObject dependencyObject,
            DispatcherPriority dispatcherPriority,
            [NotNull] Action<TDependencyObject> action)
            where TDependencyObject : DependencyObject
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var dispatcher = dependencyObject.Dispatcher;
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (dispatcher.CheckAccess())
            {
                action(dependencyObject);
            }
            else
            {
                dispatcher.BeginInvoke(dispatcherPriority, new Action(() => action(dependencyObject)));
            }
        }

        /// <summary>
        ///     Dispatches the specified dispatcher priority.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dispatcherPriority">The dispatcher priority.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dependencyObject
        ///     or
        ///     action
        ///     or
        ///     dispatcher
        /// </exception>
        public static void Dispatch(
            [NotNull] this DependencyObject dependencyObject,
            DispatcherPriority dispatcherPriority,
            [NotNull] Action action)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var dispatcher = dependencyObject.Dispatcher;
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(dispatcherPriority, action);
            }
        }
    }
}