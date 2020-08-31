using System.Windows;
using System.Windows.Controls;

namespace Desktop.Extensions.Controls
{
  /// <summary>
  /// Interaction logic for StatsGroupBox.xaml
  /// </summary>
  public partial class StatsGroupBox : UserControl
  {
    public StatsGroupBox()
    {
      InitializeComponent();
      Root.DataContext = this;
    }

    public static readonly DependencyProperty PlatformProperty =
      DependencyProperty.Register("Platform",
                                  typeof(string),
                                  typeof(StatsGroupBox));

    public static readonly DependencyProperty NotPlayedAmountProperty =
      DependencyProperty.Register("NotPlayedAmount",
                                  typeof(string),
                                  typeof(StatsGroupBox));

    public static readonly DependencyProperty PlayedAmountProperty =
      DependencyProperty.Register("PlayedAmount",
                                  typeof(string),
                                  typeof(StatsGroupBox));

    public static readonly DependencyProperty CompleteAmountProperty =
      DependencyProperty.Register("CompleteAmount",
                                  typeof(string),
                                  typeof(StatsGroupBox));

    public static readonly DependencyProperty AbandonedAmountProperty =
      DependencyProperty.Register("AbandonedAmount",
                                  typeof(string),
                                  typeof(StatsGroupBox));

    public static readonly DependencyProperty DonePercentProperty =
      DependencyProperty.Register("DonePercent",
                                  typeof(string),
                                  typeof(StatsGroupBox));
    public string Platform
    {
      get => (string)GetValue(PlatformProperty);
      set => SetValue(PlatformProperty, value);
    }

    public string NotPlayedAmount
    {
      get => (string)GetValue(NotPlayedAmountProperty);
      set => SetValue(NotPlayedAmountProperty, value);
    }

    public string PlayedAmount
    {
      get => (string)GetValue(PlayedAmountProperty);
      set => SetValue(PlayedAmountProperty, value);
    }

    public string CompleteAmount
    {
      get => (string)GetValue(CompleteAmountProperty);
      set => SetValue(CompleteAmountProperty, value);
    }

    public string AbandonedAmount
    {
      get => (string)GetValue(AbandonedAmountProperty);
      set => SetValue(AbandonedAmountProperty, value);
    }

    public string DonePercent
    {
      get => (string)GetValue(DonePercentProperty);
      set => SetValue(DonePercentProperty, value);
    }
  }
}
