using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Controls
{
  /// <summary>
  /// Interaction logic for SearchTextBox.xaml
  /// </summary>
  public partial class SearchTextBox : UserControl
  {
    #region Public Members

    public static readonly DependencyProperty SearchProperty =
      DependencyProperty.Register("SearchForText",
                                  typeof(string),
                                  typeof(SearchTextBox));

    #endregion Public Members

    #region Public Constructors

    public SearchTextBox()
    {
      InitializeComponent();
      Root.DataContext = this;
    }

    #endregion Public Constructors

    #region Public Properties

    public string SearchForText
    {
      get => (string)GetValue(SearchProperty);
      set => SetValue(SearchProperty, value);
    }

    #endregion Public Properties
  }
}