﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LagoVista.Core.Commanding
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        object _parameter;

        readonly Action _execute;
        readonly Action<object> _executeParam;
        readonly Func<object, bool> _canExecuteParam;
        readonly Func<bool> _canExecute;
        readonly Func<Task> _asyncAction;


        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action<object> execute)
        {
            _executeParam = execute;
        }

        public RelayCommand(Func<Task> asyncActionte)
        {
            _asyncAction = asyncActionte;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _executeParam = execute;
            _canExecuteParam = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public virtual bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }

            if (_canExecuteParam != null)
            {
                return _canExecuteParam(parameter);
            }

            return Enabled;
        }

        private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    RaiseCanExecuteChanged();
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            if (_executeParam != null && (_canExecuteParam == null || _canExecuteParam(parameter == null ? _parameter : parameter)))
            {
                _executeParam(parameter == null ? _parameter : parameter);
            }
            else if (_execute != null && (_canExecute == null || _canExecute()))
            {
                _execute();
            }
            else if (_asyncAction != null)
            {
                _asyncAction.Invoke();
            }
        }

        public static RelayCommand Create(Action<object> action)
        {
            return new RelayCommand(action);
        }

        public static RelayCommand Create(Action<Object> action, Object parameter)
        {
            return new RelayCommand(action) { _parameter = parameter };
        }

        public static RelayCommand Create(Action action)
        {
            return new RelayCommand(action);
        }

        public static RelayCommand CreateAsync(Func<Task> asyncAction)
        {
            return new RelayCommand(asyncAction);
        }
    }
}
