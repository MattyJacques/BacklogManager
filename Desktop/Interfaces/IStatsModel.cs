using Desktop.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
