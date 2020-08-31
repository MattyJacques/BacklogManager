using System;
using System.Windows.Input;

namespace Desktop.Extensions.Helpers
{
  public class RelayCommand : ICommand
  {
    private Action<object> _execute;
    private Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
      this._execute = execute;
      this._canExecute = canExecute;
    } // Constructor

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    } // CanExecuteChanged

    public bool CanExecute(object parameter)
    {
      return this._canExecute == null || this._canExecute(parameter);
    } // CanExecute

    public void Execute(object parameter)
    {
      this._execute(parameter);
    } // Execute
  }
}
