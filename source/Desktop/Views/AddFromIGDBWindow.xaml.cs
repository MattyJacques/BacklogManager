using Desktop.ViewModels;
using System.Windows;

namespace Desktop.Views
{
  /// <summary>
  /// Interaction logic for AddFromIGDBWindow.xaml
  /// </summary>
  public partial class AddFromIGDBWindow : Window
  {
    #region Public Constructors

    public AddFromIGDBWindow(AddFromIGDBViewModel viewModel)
    {
      InitializeComponent();

      DataContext = viewModel;
      viewModel.CloseAction = Close;
    }

    #endregion Public Constructors
  }
}