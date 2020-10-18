using Desktop.Data.Types;

namespace Desktop.Interfaces
{
  public interface IStatsModel
  {
    #region Public Methods

    /// <summary>
    /// Get the updated stats
    /// </summary>
    /// <returns></returns>
    StatsCollection GetStats();

    #endregion Public Methods
  }
}