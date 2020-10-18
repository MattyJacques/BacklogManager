using System.Windows;

namespace Desktop.Extensions.Properties
{
  internal class ReturnDialogResult
  {
    #region Public Members

    public static readonly DependencyProperty DialogResultProperty =
      DependencyProperty.RegisterAttached(
      "DialogResult",
      typeof(bool?),
      typeof(ReturnDialogResult),
      new PropertyMetadata(DialogResultChanged));

    #endregion Public Members

    #region Public Methods

    public static void SetDialogResult(Window target, bool? value)
    {
      target.SetValue(DialogResultProperty, value);
    }

    #endregion Public Methods

    #region Private Methods

    private static void DialogResultChanged(DependencyObject dObject, DependencyPropertyChangedEventArgs e)
    {
      if (dObject is Window window)
      {
        window.DialogResult = e.NewValue as bool?;
      }
    }

    #endregion Private Methods
  }
}