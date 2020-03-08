// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BusyIndicatorsGui
{
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using JetBrains.Annotations;

    public class MainViewModel : INotifyPropertyChanged
    {
        private bool isBusy;

        public MainViewModel()
        {
            this.BusyCommand = new RelayCommand<object>(this.OnBusy);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BusyCommand { get; }

        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                if (value == this.isBusy)
                {
                    return;
                }

                this.isBusy = value;
                this.OnPropertyChanged();
            }
        }

        private void OnBusy(object obj)
        {
            var b = (bool)obj;
            this.IsBusy = b;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _execute;

        private readonly Predicate<T> _canExecute;

        #endregion Fields

        #region Constructors

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            this._execute((T)parameter);
        }

        #endregion ICommand Members
    }
}