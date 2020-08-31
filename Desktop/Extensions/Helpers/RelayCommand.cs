using System;
using System.Windows.Input;

namespace Desktop.Extensions.Helpers
{
  public class RelayCommand : ICommand
  {
    #region Private Members

    private readonly Func<object, bool> _canExecute;
    private readonly Action<object> _execute;

    #endregion Private Members

    #region Public Constructors

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
      _execute = execute;
      _canExecute = canExecute;
    }

    #endregion Public Constructors

    #region Public Events

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    #endregion Public Events

    #region Public Methods

    public bool CanExecute(object parameter)
    {
      return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
      _execute(parameter);
    }

    #endregion Public Methods
  }
}