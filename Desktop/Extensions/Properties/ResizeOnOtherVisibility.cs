using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Properties
{
  class ResizeOnOtherVisibility : DependencyObject
  {
    public static readonly DependencyProperty DesiredHeightProperty =
      DependencyProperty.RegisterAttached("DesiredHeight",
        typeof(double),
        typeof(ResizeOnOtherVisibility),
        new PropertyMetadata(DesiredHeightChanged));

    public static readonly DependencyProperty ResizeTriggerProperty =
      DependencyProperty.RegisterAttached("ResizeTrigger",
        typeof(bool),
        typeof(ResizeOnOtherVisibility),
        new PropertyMetadata(ResizeTriggerChanged));

    private static void DesiredHeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ResizeTriggerChanged(obj, e);
    }

    private static void ResizeTriggerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement element = obj as FrameworkElement;
      Panel parentPanel = element.Parent as Panel;
      double heightRemaining = parentPanel.Height;

      foreach (FrameworkElement childElement in parentPanel.Children)
      {
        if (element.Name != childElement.Name && childElement.Visibility != Visibility.Collapsed)
        {
          heightRemaining -= childElement.Height; 
        }
      }

      element.Height = heightRemaining;
    }

    public static double GetDesiredHeight(DependencyObject obj)
    {
      return (double)obj.GetValue(DesiredHeightProperty);
    }

    public static void SetDesiredHeight(DependencyObject obj, double value)
    {
      obj.SetValue(DesiredHeightProperty, value);
    }

    public static bool GetResizeTrigger(DependencyObject obj)
    {
      return (bool)obj.GetValue(ResizeTriggerProperty);
    }

    public static void SetResizeTrigger(DependencyObject obj, bool value)
    {
      obj.SetValue(ResizeTriggerProperty, value);
    }
  }
}
