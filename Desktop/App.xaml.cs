using Desktop.Models;
using Desktop.ViewModels;
using Desktop.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
