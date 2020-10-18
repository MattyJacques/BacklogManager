using System.ComponentModel;

namespace Database.Game.Models
{
  public enum Status
  {
    [Description("Not Played")]
    NotPlayed,

    Played,
    Complete,
    Abandoned
  }

  public class GameDatabaseEntry
  {
    #region Public Properties

    public string AddedDate { get; set; }
    public string DownloadedData { get; set; }
    public string GameName { get; set; }
    public string OwnedStatus { get; set; }
    public string PC { get; set; }
    public string PlayedStatus { get; set; }
    public string PS3 { get; set; }
    public string PS4 { get; set; }
    public string PSVita { get; set; }

    #endregion Public Properties
  }
}