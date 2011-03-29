using System;
using System.Diagnostics;
using System.Windows.Input;

namespace BCharppe.WPFColorManager
{
    /// <summary>
    /// Taken from http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090048
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}