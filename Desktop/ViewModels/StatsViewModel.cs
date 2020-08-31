using Desktop.Data.Types;
using Desktop.Interfaces;
using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
  public class StatsViewModel : ViewModelBase, IPageViewModel
  {
    #region Variables

    private string _name = "Stats";
    private readonly IStatsModel _model;
    private readonly StatsCollection _stats;

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
    public string Name { get => _name; set => _name = value; }

    /// <summary>
    /// Return the amount of games that are not played on PC
    /// </summary>
    public int NotPlayedAmountPC => _stats.PC.NotPlayedAmount;

    /// <summary>
    /// Return the amount of games that are played on PC
    /// </summary>
    public int PlayedAmountPC => _stats.PC.PlayedAmount;

    /// <summary>
    /// Return the amount of games that are completed on PC
    /// </summary>
    public int CompleteAmountPC => _stats.PC.CompleteAmount;

    /// <summary>
    /// Return the amount of games that are abandoned on PC
    /// </summary>
    public int AbandonedAmountPC => _stats.PC.AbandonedAmount;

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PC
    /// </summary>
    public string DonePercentPC => string.Format("{0:0.00}%", _stats.PC.DonePercent);

    /// <summary>
    /// Return the amount of games that are not played on PS4
    /// </summary>
    public int NotPlayedAmountPS4 => _stats.PS4.NotPlayedAmount;

    /// <summary>
    /// Return the amount of games that are played on PS4
    /// </summary>
    public int PlayedAmountPS4 => _stats.PS4.PlayedAmount;

    /// <summary>
    /// Return the amount of games that are completed on PS4
    /// </summary>
    public int CompleteAmountPS4 => _stats.PS4.CompleteAmount;

    /// <summary>
    /// Return the amount of games that are abandoned on PS4
    /// </summary>
    public int AbandonedAmountPS4 => _stats.PS4.AbandonedAmount;

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PS4
    /// </summary>
    public string DonePercentPS4 => string.Format("{0:0.00}%", _stats.PS4.DonePercent);

    /// <summary>
    /// Return the amount of games that are not played on PS3
    /// </summary>
    public int NotPlayedAmountPS3 => _stats.PS3.NotPlayedAmount;

    /// <summary>
    /// Return the amount of games that are played on PS3
    /// </summary>
    public int PlayedAmountPS3 => _stats.PS3.PlayedAmount;

    /// <summary>
    /// Return the amount of games that are completed on PS3
    /// </summary>
    public int CompleteAmountPS3 => _stats.PS3.CompleteAmount;

    /// <summary>
    /// Return the amount of games that are abandoned on PS3
    /// </summary>
    public int AbandonedAmountPS3 => _stats.PS3.AbandonedAmount;

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PS3
    /// </summary>
    public string DonePercentPS3 => string.Format("{0:0.00}%", _stats.PS3.DonePercent);

    /// <summary>
    /// Return the amount of games that are not played on PS Vita
    /// </summary>
    public int NotPlayedAmountVita => _stats.PSVita.NotPlayedAmount;

    /// <summary>
    /// Return the amount of games that are played on PS Vita
    /// </summary>
    public int PlayedAmountVita => _stats.PSVita.PlayedAmount;

    /// <summary>
    /// Return the amount of games that are completed on PS Vita
    /// </summary>
    public int CompleteAmountVita => _stats.PSVita.CompleteAmount;

    /// <summary>
    /// Return the amount of games that are abandoned on PS Vita
    /// </summary>
    public int AbandonedAmountVita => _stats.PSVita.AbandonedAmount;

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned on PS Vita
    /// </summary>
    public string DonePercentVita => string.Format("{0:0.00}%", _stats.PSVita.DonePercent);

    /// <summary>
    /// Return the total amount of games that are not played
    /// </summary>
    public int NotPlayedAmountTotal => _stats.TotalNotPlayed;

    /// <summary>
    /// Return the total amount of games that are played
    /// </summary>
    public int PlayedAmountTotal => _stats.TotalPlayed;

    /// <summary>
    /// Return the total amount of games that are completed
    /// </summary>
    public int CompleteAmountTotal => _stats.TotalComplete;

    /// <summary>
    /// Return the total amount of games that are abandoned
    /// </summary>
    public int AbandonedAmountTotal => _stats.TotalAbandoned;

    /// <summary>
    /// Return the percentage of games that are Complete or Abandoned
    /// </summary>
    public string DonePercentTotal => string.Format("{0:0.00}%", _stats.TotalDonePercent);

    #endregion // Properties

    #region Commands



    #endregion // Commands

    #region Implementation



    #endregion // Implementation
  }
}
