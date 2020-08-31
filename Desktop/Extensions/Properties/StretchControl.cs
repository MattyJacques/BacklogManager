using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Properties
{
  internal class StretchControl : DependencyObject
  {
    #region Members

    private static string _controlName = string.Empty;

    #endregion Members

    #region Properties

    public static readonly DependencyProperty ShouldResizeProperty =
      DependencyProperty.RegisterAttached("ShouldResize",
        typeof(bool),
        typeof(StretchControl),
        new PropertyMetadata(ResizeChanged));

    public static readonly DependencyProperty ResizeTriggerProperty =
      DependencyProperty.RegisterAttached("ResizeTrigger",
        typeof(bool),
        typeof(StretchControl),
        new PropertyMetadata(ResizeTriggerChanged));

    #endregion // Properties

    #region Events

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
    } // ResizeTriggerChanged

    private static void OnParentSizeChanged(object sender, SizeChangedEventArgs e)
    {
      CalculateSize(sender as Panel);
    } // OnParentSizeChanged

    private static void OnParentLoaded(object sender, RoutedEventArgs e)
    {
      CalculateSize(sender as Panel);
    } // OnParentLoaded

    #endregion // Events

    #region Implementation

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
    } //CalculateSize

    #endregion // Implementation

    #region Gets/Sets

    public static bool GetShouldResize(DependencyObject obj)
    {
      return (bool)obj.GetValue(ShouldResizeProperty);
    }

    public static void SetShouldResize(DependencyObject obj, bool value)
    {
      obj.SetValue(ShouldResizeProperty, value);
    }

    public static bool GetResizeTrigger(DependencyObject obj)
    {
      return (bool)obj.GetValue(ResizeTriggerProperty);
    }

    public static void SetResizeTrigger(DependencyObject obj, bool value)
    {
      obj.SetValue(ResizeTriggerProperty, value);
    }

    #endregion // Gets/Sets
  }
}
