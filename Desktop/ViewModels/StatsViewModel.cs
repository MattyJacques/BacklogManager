using Desktop.Data.Types;
using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop.ViewModels
{
  public class StatsViewModel : ViewModelBase, IPageViewModel
  {
    #region Variables

    

    #endregion // Variables

    #region Construction

    

    #endregion // Construction

    #region Properties

    private string _name = "Stats";
    /// <summary>
    /// The name of the view model
    /// </summary>
    public string Name { get { return _name; } set { _name = value; } }

    #endregion // Properties

    #region Commands



    #endregion // Commands

    #region Implementation

    

    #endregion // Implementation
  }
}
