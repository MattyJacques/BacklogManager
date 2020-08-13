using System;
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

    public String GameName { get; set; }

    public String AddedDate { get; set; }

    public String PC { get; set; }

    public String PS3 { get; set; }

    public String PS4 { get; set; }

    public String PSVita { get; set; }

    public String OwnedStatus { get; set; }

    public String PlayedStatus { get; set; }

    #endregion // Members
  }
}
