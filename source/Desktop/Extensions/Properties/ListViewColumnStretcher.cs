﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Properties
{
  internal class ListViewColumnStretcher
  {
    #region Public Members

    /// <summary>
    /// IsStretched Dependency property which can be attached to GridViewColumns.
    /// </summary>
    public static readonly DependencyProperty StretchProperty =
      DependencyProperty.RegisterAttached("Stretch",
      typeof(bool),
      typeof(ListViewColumnStretcher),
      new UIPropertyMetadata(true, null, OnCoerceStretch));

    #endregion Public Members

    #region Public Methods

    /// <summary>
    /// Get whether to stretch or not
    /// </summary>
    /// <param name="obj">Dependency object</param>
    /// <returns></returns>
    public static bool GetStretch(DependencyObject obj)
    {
      return (bool)obj.GetValue(StretchProperty);
    }

    /// <summary>
    /// Check whether the property is attached to a ListView
    /// </summary>
    /// <param name="source">Object to check if it is a ListView</param>
    /// <param name="value">Stretch value, not used</param>
    /// <returns></returns>
    public static object OnCoerceStretch(DependencyObject source, object value)
    {
      ListView listView = (source as ListView);

      // Ensure we don't have an invalid dependency object of type ListView.
      if (listView == null)
      {
        throw new ArgumentException("This property may only be used on ListViews");
      }

      //Setup our event handlers for this list view.
      listView.Loaded += new RoutedEventHandler(ListView_Loaded);
      listView.SizeChanged += new SizeChangedEventHandler(ListView_SizeChanged);
      return value;
    }

    /// <summary>
    /// Set whether to stretch or not
    /// </summary>
    /// <param name="obj">Dependency object</param>
    /// <param name="value">Whether to stretch width or not</param>
    public static void SetStretch(DependencyObject obj, bool value)
    {
      obj.SetValue(StretchProperty, value);
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Set column widths when ListView is loaded
    /// </summary>
    /// <param name="sender">ListView that just loaded</param>
    /// <param name="e">Event arguments</param>
    private static void ListView_Loaded(object sender, RoutedEventArgs e)
    {
      ListView listView = sender as ListView;
      SetColumnWidths(listView);
    }

    /// <summary>
    /// Set the column width when the ListView size has changed
    /// </summary>
    /// <param name="sender">ListView that just changed size</param>
    /// <param name="e">Event arguments</param>
    private static void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      ListView listView = (sender as ListView);
      if (listView.IsLoaded)
      {
        SetColumnWidths(listView);
      }
    }

    /// <summary>
    /// Set column widths
    /// </summary>
    /// <param name="listView">ListView to set column widths</param>
    private static void SetColumnWidths(ListView listView)
    {
      // Pull the stretch columns from the tag property.
      List<GridViewColumn> columns = listView.Tag as List<GridViewColumn>;
      double specifiedWidth = 0;
      GridView gridView = listView.View as GridView;

      if (gridView != null)
      {
        if (columns == null)
        {
          // Instance if its our first run.
          columns = new List<GridViewColumn>();
          // Get all columns with no width having been set.
          foreach (GridViewColumn column in gridView.Columns)
          {
            if (!(column.Width >= 0))
            {
              columns.Add(column);
            }
            else
            {
              specifiedWidth += column.ActualWidth;
            }
          }
        }
        else
        {
          // Get all columns with no width having been set.
          foreach (GridViewColumn column in gridView.Columns)
          {
            if (!columns.Contains(column))
            {
              specifiedWidth += column.ActualWidth;
            }
          }
        }

        // Allocate remaining space equally.
        foreach (GridViewColumn column in columns)
        {
          double newWidth = (listView.ActualWidth - specifiedWidth) / columns.Count;
          if (newWidth >= 10 + SystemParameters.VerticalScrollBarWidth)
          {
            column.Width = newWidth - 10 - SystemParameters.VerticalScrollBarWidth;
          }
        }

        // Store the columns in the TAG property for later use.
        listView.Tag = columns;
      }
    }

    #endregion Private Methods
  }
}