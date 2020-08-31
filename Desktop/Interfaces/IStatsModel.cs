using Desktop.Data.Types;

namespace Desktop.Interfaces
{
  public interface IStatsModel
  {
    /// <summary>
    /// Get the updated stats
    /// </summary>
    /// <returns></returns>
    StatsCollection GetStats();
  }
}
