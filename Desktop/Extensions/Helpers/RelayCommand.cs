using System;
using System.Windows.Input;

namespace Desktop.Extensions.Helpers
{
  public class RelayCommand : ICommand
  {
    private Action<object> execute;
    private Func<object, bool> canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    } // Constructor

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    } // CanExecuteChanged

    public bool CanExecute(object parameter)
    {
      return this.canExecute == null || this.canExecute(parameter);
    } // CanExecute

    public void Execute(object parameter)
    {
      this.execute(parameter);
    } // Execute
  }
}
