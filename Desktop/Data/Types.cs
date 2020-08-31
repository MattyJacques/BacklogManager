using System.ComponentModel;

namespace Desktop.Data.Types
{
  public enum Status
  {
    [Description("Not Played")]
    NotPlayed,
    Played,
    Complete,
    Abandoned
  }

  public enum ColIndex
  {
    GameName = 0,
    AddedDate,
    PC,
    PS3,
    PS4,
    PSVita,
    OwnedStatus,
    PlayedStatus
  }

  public class GameDatabaseEntry
  {
    #region Members

    public string GameName { get; set; }
    public string AddedDate { get; set; }
    public string PC { get; set; }
    public string PS3 { get; set; }
    public string PS4 { get; set; }
    public string PSVita { get; set; }
    public string OwnedStatus { get; set; }
    public string PlayedStatus { get; set; }

    #endregion // Members
  }

  public class StatsCollection
  {
    public StatsCollection()
    {
      PC = new PlatformStats();
      PS4 = new PlatformStats();
      PS3 = new PlatformStats();
      PSVita = new PlatformStats();
    } // Constructor

    public PlatformStats PC { get; set; }
    public PlatformStats PS4 { get; set; }
    public PlatformStats PS3 { get; set; }
    public PlatformStats PSVita { get; set; }

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

    /// <summary>
    /// Return the total games complete by adding values from all PlatformStats
    /// </summary>
    public int TotalComplete => PC.CompleteAmount +
               PS4.CompleteAmount +
               PS3.CompleteAmount +
               PSVita.CompleteAmount;

    /// <summary>
    /// Return the total games abandoned by adding values from all PlatformStats
    /// </summary>
    public int TotalAbandoned => PC.AbandonedAmount +
               PS4.AbandonedAmount +
               PS3.AbandonedAmount +
               PSVita.AbandonedAmount;

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
  }

  public class PlatformStats
  {
    public int NotPlayedAmount { get; set; }
    public int PlayedAmount { get; set; }
    public int CompleteAmount { get; set; }
    public int AbandonedAmount { get; set; }
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
  }
}
