﻿using System.ComponentModel;

namespace Desktop.Data.Types
{
  public class PlatformStats
  {
    #region Public Properties

    public int AbandonedAmount { get; set; }
    public int CompleteAmount { get; set; }

    public float DonePercent
    {
      get
      {
        int totalNotDone = NotPlayedAmount + PlayedAmount;
        int totalDone = CompleteAmount + AbandonedAmount;
        int totalGames = totalNotDone + totalDone;
        float result = 0;

        if (totalDone == totalGames)
        {
          result = 100;
        }
        else if (totalDone > 0 && totalGames > 0)
        {
          result = ((float)totalDone / totalGames) * 100;
        }

        return result;
      }
    }

    public int NotPlayedAmount { get; set; }
    public int PlayedAmount { get; set; }

    #endregion Public Properties
  }

  public class StatsCollection
  {
    #region Public Constructors

    public StatsCollection()
    {
      PC = new PlatformStats();
      PS4 = new PlatformStats();
      PS3 = new PlatformStats();
      PSVita = new PlatformStats();
    }

    #endregion Public Constructors

    #region Public Properties

    public PlatformStats PC { get; set; }
    public PlatformStats PS3 { get; set; }
    public PlatformStats PS4 { get; set; }
    public PlatformStats PSVita { get; set; }

    /// <summary>
    /// Return the total games abandoned by adding values from all PlatformStats
    /// </summary>
    public int TotalAbandoned => PC.AbandonedAmount +
               PS4.AbandonedAmount +
               PS3.AbandonedAmount +
               PSVita.AbandonedAmount;

    /// <summary>
    /// Return the total games complete by adding values from all PlatformStats
    /// </summary>
    public int TotalComplete => PC.CompleteAmount +
               PS4.CompleteAmount +
               PS3.CompleteAmount +
               PSVita.CompleteAmount;

    /// <summary>
    /// Return the total games complete or abandoned by adding values from all PlatformStats
    /// </summary>
    public float TotalDonePercent
    {
      get
      {
        int totalNotDone = TotalNotPlayed + TotalPlayed;
        int totalDone = TotalComplete + TotalAbandoned;
        int totalGames = totalNotDone + totalDone;

        return ((float)totalDone / totalGames) * 100;
      }
    }

    /// <summary>
    /// Return the total games not played by adding values from all PlatformStats
    /// </summary>
    public int TotalNotPlayed => PC.NotPlayedAmount +
               PS4.NotPlayedAmount +
               PS3.NotPlayedAmount +
               PSVita.NotPlayedAmount;

    /// <summary>
    /// Return the total games played by adding values from all PlatformStats
    /// </summary>
    public int TotalPlayed => PC.PlayedAmount +
               PS4.PlayedAmount +
               PS3.PlayedAmount +
               PSVita.PlayedAmount;

    #endregion Public Properties
  }
}