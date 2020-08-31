using Desktop.ViewModels;
using Desktop.Views;
using System.Windows;

namespace Desktop
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      ApplicationWindow app = new ApplicationWindow
      {
        DataContext = new ApplicationViewModel()
      };

      app.Show();
    }
  }
}
