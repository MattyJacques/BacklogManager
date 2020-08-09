using Desktop.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Desktop.Views
{
  public partial class GameListWindow : Window
  {
    public GameListWindow(GameListViewModel viewModel)
    {
      this.DataContext = viewModel;
      InitializeComponent();
    }
  }
}
