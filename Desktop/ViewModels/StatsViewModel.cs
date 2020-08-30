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

    private string _name = "Stats";
    private IStatsModel _model;
    private StatsCollection _stats;

    #endregion // Variables

    #region Construction

    public StatsViewModel(IStatsModel model)
    {
      _model = model;
      _stats = model.GetStats();
    } // Constructor

    #endregion // Construction

    #region Properties

    /// <summary>
    /// The name of the view model
    /// </summary>
    public string Name { get { return _name; } set { _name = value; } }

    /// <summary>
    /// Return the amount of games that are not played on PC
    /// </summary>
    public int NotPlayedAmountPC { get { return _stats.PC.NotPlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are played on PC
    /// </summary>
    public int PlayedAmountPC { get { return _stats.PC.PlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are completed on PC
    /// </summary>
    public int CompleteAmountPC { get { return _stats.PC.CompleteAmount; } }

    /// <summary>
    /// Return the amount of games that are abandoned on PC
    /// </summary>
    public int AbandonedAmountPC { get { return _stats.PC.AbandonedAmount; } }

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PC
    /// </summary>
    public string DonePercentPC { get { return String.Format("{0:0.00}%", _stats.PC.DonePercent); } }

    /// <summary>
    /// Return the amount of games that are not played on PS4
    /// </summary>
    public int NotPlayedAmountPS4 { get { return _stats.PS4.NotPlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are played on PS4
    /// </summary>
    public int PlayedAmountPS4 { get { return _stats.PS4.PlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are completed on PS4
    /// </summary>
    public int CompleteAmountPS4 { get { return _stats.PS4.CompleteAmount; } }

    /// <summary>
    /// Return the amount of games that are abandoned on PS4
    /// </summary>
    public int AbandonedAmountPS4 { get { return _stats.PS4.AbandonedAmount; } }

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PS4
    /// </summary>
    public string DonePercentPS4 { get { return String.Format("{0:0.00}%", _stats.PS4.DonePercent); } }

    /// <summary>
    /// Return the amount of games that are not played on PS3
    /// </summary>
    public int NotPlayedAmountPS3 { get { return _stats.PS3.NotPlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are played on PS3
    /// </summary>
    public int PlayedAmountPS3 { get { return _stats.PS3.PlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are completed on PS3
    /// </summary>
    public int CompleteAmountPS3 { get { return _stats.PS3.CompleteAmount; } }

    /// <summary>
    /// Return the amount of games that are abandoned on PS3
    /// </summary>
    public int AbandonedAmountPS3 { get { return _stats.PS3.AbandonedAmount; } }

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PS3
    /// </summary>
    public string DonePercentPS3 { get { return String.Format("{0:0.00}%", _stats.PS3.DonePercent); } }

    /// <summary>
    /// Return the amount of games that are not played on PS Vita
    /// </summary>
    public int NotPlayedAmountVita { get { return _stats.PSVita.NotPlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are played on PS Vita
    /// </summary>
    public int PlayedAmountVita { get { return _stats.PSVita.PlayedAmount; } }

    /// <summary>
    /// Return the amount of games that are completed on PS Vita
    /// </summary>
    public int CompleteAmountVita { get { return _stats.PSVita.CompleteAmount; } }

    /// <summary>
    /// Return the amount of games that are abandoned on PS Vita
    /// </summary>
    public int AbandonedAmountVita { get { return _stats.PSVita.AbandonedAmount; } }

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PS Vita
    /// </summary>
    public string DonePercentVita { get { return String.Format("{0:0.00}%", _stats.PSVita.DonePercent); } }

    /// <summary>
    /// Return the total amount of games that are not played
    /// </summary>
    public int NotPlayedAmountTotal { get { return _stats.TotalNotPlayed; } }

    /// <summary>
    /// Return the total amount of games that are played
    /// </summary>
    public int PlayedAmountTotal { get { return _stats.TotalPlayed; } }

    /// <summary>
    /// Return the total amount of games that are completed
    /// </summary>
    public int CompleteAmountTotal { get { return _stats.TotalComplete; } }

    /// <summary>
    /// Return the total amount of games that are abandoned
    /// </summary>
    public int AbandonedAmountTotal { get { return _stats.TotalAbandoned; } }

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned
    /// </summary>
    public string DonePercentTotal { get { return String.Format("{0:0.00}%", _stats.TotalDonePercent); } }

    #endregion // Properties

    #region Commands



    #endregion // Commands

    #region Implementation



    #endregion // Implementation
  }
}
