using Desktop.Data;
using Desktop.Data.Types;
using Desktop.Interfaces;

namespace Desktop.Models
{
  internal class Stats : IStatsModel
  {
    #region Members

    private StatsCollection _stats = new StatsCollection();
    private readonly GameCollectionDatabase _database = new GameCollectionDatabase();

    #endregion // Members

    #region Public Methods

    /// <summary>
    /// Get the updated stats
    /// </summary>
    /// <returns></returns>
    public StatsCollection GetStats()
    {
      _stats = new StatsCollection();

      _stats.PC.NotPlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PC), Status.NotPlayed.ToString());
      _stats.PC.PlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PC), Status.Played.ToString());
      _stats.PC.CompleteAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PC), Status.Complete.ToString());
      _stats.PC.AbandonedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PC), Status.Abandoned.ToString());

      _stats.PS4.NotPlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS4), Status.NotPlayed.ToString());
      _stats.PS4.PlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS4), Status.Played.ToString());
      _stats.PS4.CompleteAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS4), Status.Complete.ToString());
      _stats.PS4.AbandonedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS4), Status.Abandoned.ToString());

      _stats.PS3.NotPlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS3), Status.NotPlayed.ToString());
      _stats.PS3.PlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS3), Status.Played.ToString());
      _stats.PS3.CompleteAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS3), Status.Complete.ToString());
      _stats.PS3.AbandonedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PS3), Status.Abandoned.ToString());

      _stats.PSVita.NotPlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PSVita), Status.NotPlayed.ToString());
      _stats.PSVita.PlayedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PSVita), Status.Played.ToString());
      _stats.PSVita.CompleteAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PSVita), Status.Complete.ToString());
      _stats.PSVita.AbandonedAmount = _database.GetAmountWithPlatformStatus(nameof(_stats.PSVita), Status.Abandoned.ToString());

      return _stats;
    }

    /// <summary>
    /// Get the amount of games that are not played for PC
    /// </summary>
    public int GetPCNotPlayedAmount => _stats.PC.NotPlayedAmount;

    /// <summary>
    /// Get the amount of games that are played for PC
    /// </summary>
    public int GetPCPlayedAmount => _stats.PC.PlayedAmount;

    /// <summary>
    /// Get the amount of games that are complete for PC
    /// </summary>
    public int GetPCCompleteAmount => _stats.PC.CompleteAmount;

    /// <summary>
    /// Get the amount of games that are abandoned for PC
    /// </summary>
    public int GetPCAbandonedAmount => _stats.PC.AbandonedAmount;

    /// <summary>
    /// Get the percentage of PC games that are complete or abandoned
    /// </summary>
    public float GetPCDonePercent => _stats.PC.DonePercent;

    /// <summary>
    /// Get the amount of games that are not played for PS4
    /// </summary>
    public int GetPS4NotPlayedAmount => _stats.PS4.NotPlayedAmount;

    /// <summary>
    /// Get the amount of games that are played for PS4
    /// </summary>
    public int GetPS4PlayedAmount => _stats.PS4.PlayedAmount;

    /// <summary>
    /// Get the amount of games that are complete for PS4
    /// </summary>
    public int GetPS4CompleteAmount => _stats.PS4.CompleteAmount;

    /// <summary>
    /// Get the amount of games that are abandoned for PS4
    /// </summary>
    public int GetPS4AbandonedAmount => _stats.PS4.AbandonedAmount;

    /// <summary>
    /// Get the percentage of PS4 games that are complete or abandoned
    /// </summary>
    public float GetPS4DonePercent => _stats.PS4.DonePercent;

    /// <summary>
    /// Get the amount of games that are not played for PS3
    /// </summary>
    public int GetPS3NotPlayedAmount => _stats.PS3.NotPlayedAmount;

    /// <summary>
    /// Get the amount of games that are played for PS3
    /// </summary>
    public int GetPS3PlayedAmount => _stats.PS3.PlayedAmount;

    /// <summary>
    /// Get the amount of games that are complete for PS3
    /// </summary>
    public int GetPS3CompleteAmount => _stats.PS3.CompleteAmount;

    /// <summary>
    /// Get the amount of games that are abandoned for PS3
    /// </summary>
    public int GetPS3AbandonedAmount => _stats.PS3.AbandonedAmount;

    /// <summary>
    /// Get the percentage of PS3 games that are complete or abandoned
    /// </summary>
    public float GetPS3DonePercent => _stats.PS3.DonePercent;

    /// <summary>
    /// Get the amount of games that are not played for Vita
    /// </summary>
    public int GetVitaNotPlayedAmount => _stats.PSVita.NotPlayedAmount;

    /// <summary>
    /// Get the amount of games that are played for Vita
    /// </summary>
    public int GetVitaPlayedAmount => _stats.PSVita.PlayedAmount;

    /// <summary>
    /// Get the amount of games that are complete for Vita
    /// </summary>
    public int GetVitaCompleteAmount => _stats.PSVita.CompleteAmount;

    /// <summary>
    /// Get the amount of games that are abandoned for Vita
    /// </summary>
    public int GetVitaAbandonedAmount => _stats.PSVita.AbandonedAmount;

    /// <summary>
    /// Get the percentage of Vita games that are complete or abandoned
    /// </summary>
    public float GetVitaDonePercent => _stats.PSVita.DonePercent;

    /// <summary>
    /// Get the total amount of games that are not played
    /// </summary>
    public int GetTotalNotPlayedAmount => _stats.TotalNotPlayed;

    /// <summary>
    /// Get the total amount of games that are played
    /// </summary>
    public int GetTotalPlayedAmount => _stats.TotalPlayed;

    /// <summary>
    /// Get the total amount of games that are complete
    /// </summary>
    public int GetTotalCompleteAmount => _stats.TotalComplete;

    /// <summary>
    /// Get the total amount of games that are abandoned
    /// </summary>
    public int GetTotalAbandonedAmount => _stats.TotalAbandoned;

    /// <summary>
    /// Get the percentage of Vita games that are complete or abandoned
    /// </summary>
    public float GetTotalDonePercent => _stats.TotalDonePercent;


    #endregion // Public Methods
  }
}
