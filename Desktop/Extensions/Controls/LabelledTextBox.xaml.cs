﻿using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Controls
{
  /// <summary>
  /// Interaction logic for LabelledTextBox.xaml
  /// </summary>
  public partial class LabelledTextBox : UserControl
  {
    #region Public Members

    public static readonly DependencyProperty LabelProperty =
      DependencyProperty.Register("Label",
                                  typeof(string),
                                  typeof(LabelledTextBox),
                                  new FrameworkPropertyMetadata("Unnamed Label"));

    public static readonly DependencyProperty TextProperty =
      DependencyProperty.Register("Text",
                                  typeof(string),
                                  typeof(LabelledTextBox),
                                  new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion Public Members

    #region Public Constructors

    public LabelledTextBox()
    {
      InitializeComponent();
      Root.DataContext = this;
    }

    #endregion Public Constructors

    #region Public Properties

    public string Label
    {
      get => (string)GetValue(LabelProperty);
      set => SetValue(LabelProperty, value);
    }

    public string Text
    {
      get => (string)GetValue(TextProperty);
      set => SetValue(TextProperty, value);
    }

    #endregion Public Properties
  }
}