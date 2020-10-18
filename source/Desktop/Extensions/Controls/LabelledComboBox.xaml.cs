using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Controls
{
  /// <summary>
  /// Interaction logic for LabelledComboBox.xaml
  /// </summary>
  public partial class LabelledComboBox : UserControl
  {
    #region Public Members

    public static readonly DependencyProperty ItemSourceProperty =
      DependencyProperty.Register("ItemSource",
                                  typeof(IEnumerable),
                                  typeof(LabelledComboBox),
                                  new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty LabelProperty =
      DependencyProperty.Register("Label",
                                  typeof(string),
                                  typeof(LabelledComboBox),
                                  new FrameworkPropertyMetadata("Unnamed Label"));

    public static readonly DependencyProperty SelectedItemProperty =
      DependencyProperty.Register("SelectedItem",
                                  typeof(object),
                                  typeof(LabelledComboBox),
                                  new PropertyMetadata(null));

    #endregion Public Members

    #region Public Constructors

    public LabelledComboBox()
    {
      InitializeComponent();
      Root.DataContext = this;
    }

    #endregion Public Constructors

    #region Public Properties

    public IEnumerable ItemSource
    {
      get => (IEnumerable)GetValue(ItemSourceProperty);
      set => SetValue(ItemSourceProperty, value);
    }

    public string Label
    {
      get => (string)GetValue(LabelProperty);
      set => SetValue(LabelProperty, value);
    }

    public object SelectedItem
    {
      get => GetValue(SelectedItemProperty);
      set => SetValue(SelectedItemProperty, value);
    }

    #endregion Public Properties
  }
}