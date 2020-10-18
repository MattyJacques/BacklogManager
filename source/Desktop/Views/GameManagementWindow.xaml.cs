using Desktop.ViewModels;
using System.Windows;

namespace Desktop.Views
{
  /// <summary>
  /// Interaction logic for GameManagementWindow.xaml
  /// </summary>
  public partial class GameManagementWindow : Window
  {
    #region Public Constructors

    public GameManagementWindow(GameManagementViewModel viewModel)
    {
      InitializeComponent();

      DataContext = viewModel;
      viewModel.CloseAction = Close;
    }

    #endregion Public Constructors
  }
}