using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Properties
{
  internal class StretchControl : DependencyObject
  {
    #region Public Members

    public static readonly DependencyProperty ResizeTriggerProperty =
      DependencyProperty.RegisterAttached("ResizeTrigger",
        typeof(bool),
        typeof(StretchControl),
        new PropertyMetadata(ResizeTriggerChanged));

    public static readonly DependencyProperty ShouldResizeProperty =
      DependencyProperty.RegisterAttached("ShouldResize",
        typeof(bool),
        typeof(StretchControl),
        new PropertyMetadata(ResizeChanged));

    #endregion Public Members

    #region Private Members

    private static string _controlName = string.Empty;

    #endregion Private Members

    #region Public Methods

    public static bool GetResizeTrigger(DependencyObject obj)
    {
      return (bool)obj.GetValue(ResizeTriggerProperty);
    }

    public static bool GetShouldResize(DependencyObject obj)
    {
      return (bool)obj.GetValue(ShouldResizeProperty);
    }

    public static void SetResizeTrigger(DependencyObject obj, bool value)
    {
      obj.SetValue(ResizeTriggerProperty, value);
    }

    public static void SetShouldResize(DependencyObject obj, bool value)
    {
      obj.SetValue(ShouldResizeProperty, value);
    }

    #endregion Public Methods

    #region Private Methods

    private static void CalculateSize(Panel parentPanel)
    {
      double heightRemaining = parentPanel.ActualHeight;
      FrameworkElement elementToStretch = null;

      foreach (FrameworkElement childElement in parentPanel.Children)
      {
        if (_controlName != childElement.Name)
        {
          if (childElement.Visibility != Visibility.Collapsed)
          {
            heightRemaining -= childElement.ActualHeight;
          }
        }
        else
        {
          elementToStretch = childElement;
        }
      }

      if (elementToStretch != null)
      {
        elementToStretch.Height = heightRemaining;
      }
    }

    private static void OnParentLoaded(object sender, RoutedEventArgs e)
    {
      CalculateSize(sender as Panel);
    }

    private static void OnParentSizeChanged(object sender, SizeChangedEventArgs e)
    {
      CalculateSize(sender as Panel);
    }

    private static void ResizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement element = obj as FrameworkElement;
      FrameworkElement parentElement = element.Parent as FrameworkElement;
      parentElement.Loaded += new RoutedEventHandler(OnParentLoaded);
      parentElement.SizeChanged += new SizeChangedEventHandler(OnParentSizeChanged);

      _controlName = element.Name;
    } // ResizeOnStartChanged

    private static void ResizeTriggerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      _controlName = (obj as FrameworkElement).Name;
      CalculateSize((obj as FrameworkElement).Parent as Panel);
    }

    #endregion Private Methods

    // ResizeTriggerChanged

    // OnParentSizeChanged

    // OnParentLoaded

    //CalculateSize
  }
}