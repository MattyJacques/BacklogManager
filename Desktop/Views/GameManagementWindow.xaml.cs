using Desktop.ViewModels;
using System.Windows;

namespace Desktop.Views
{
  /// <summary>
  /// Interaction logic for GameManagementWindow.xaml
  /// </summary>
  public partial class GameManagementWindow : Window
  {
    public GameManagementWindow(GameManagementViewModel viewModel)
    {
      InitializeComponent();

      DataContext = viewModel;
    }
  }
}
