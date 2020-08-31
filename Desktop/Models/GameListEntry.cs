using Desktop.Data.Types;
using System;
using System.Windows;

namespace Desktop.Models
{
  public class GameListEntry
  {
    #region Construction

    public GameListEntry(GameDatabaseEntry databaseEntry)
    {
      try
      {
        Name = databaseEntry.GameName;
        IsOnPS4 = databaseEntry.PS4 == "true";
        IsOnPS3 = databaseEntry.PS3 == "true";
        IsOnPSVita = databaseEntry.PSVita == "true";
        IsOnPC = databaseEntry.PC == "true";
        Owned = databaseEntry.OwnedStatus == "true";

        string statusNoSpace = databaseEntry.PlayedStatus.Replace(" ", "");
        PlayStatus = (Status)Enum.Parse(typeof(Status), statusNoSpace, true);

        DateAdded = DateTime.Parse(databaseEntry.AddedDate);
      }
      catch (ArgumentException exception)
      {
        MessageBox.Show(exception.Message);
      }
    } // Constructor - From GameDatabaseEntry

    public GameListEntry(GameListEntry entry)
    {
      Copy(entry);
    }

    public GameListEntry()
    {

    } // Constructor - Default

    #endregion // Construction

    #region Properties

    private string _name = string.Empty;
    /// <summary>
    /// Get/set the title of the game
    /// </summary>
    public string Name { get => _name; set => _name = value; }

    private bool _isOnPS4 = false;
    /// <summary>
    /// Get/set if the game is playable on PS4
    /// </summary>
    public bool IsOnPS4 { get => _isOnPS4; set => _isOnPS4 = value; }

    private bool _isOnPS3 = false;
    /// <summary>
    /// Get/set if the game is playable on PS3
    /// </summary>
    public bool IsOnPS3 { get => _isOnPS3; set => _isOnPS3 = value; }

    private bool _isOnPSVita = false;
    /// <summary>
    /// Get/set if the game is playable on PS Vita
    /// </summary>
    public bool IsOnPSVita { get => _isOnPSVita; set => _isOnPSVita = value; }

    private bool _isOnPC = false;
    /// <summary>
    /// Get/set if the game is playable on PC
    /// </summary>
    public bool IsOnPC { get => _isOnPC; set => _isOnPC = value; }

    private Status _playStatus = Status.NotPlayed;
    /// <summary>
    /// Get/set the play status of the game
    /// </summary>
    public Status PlayStatus { get => _playStatus; set => _playStatus = value; }

    private bool _owned = false;
    /// <summary>
    /// Get/set the if the game is owned
    /// </summary>
    public bool Owned { get => _owned; set => _owned = value; }

    private DateTime _dateAdded = DateTime.Today;
    /// <summary>
    /// Get/set the date the game was added to the database
    /// </summary>
    public DateTime DateAdded { get => _dateAdded; set => _dateAdded = value; }

    #endregion // Properties

    #region Public Methods

    public GameDatabaseEntry ToDatabaseEntry()
    {
      GameDatabaseEntry entry = new GameDatabaseEntry
      {

        // ToLower for legacy compatibility
        GameName = Name,
        AddedDate = DateAdded.Date.ToShortDateString(),
        PC = IsOnPC.ToString().ToLower(),
        PS3 = IsOnPS3.ToString().ToLower(),
        PS4 = IsOnPS4.ToString().ToLower(),
        PSVita = IsOnPSVita.ToString().ToLower(),
        OwnedStatus = Owned.ToString().ToLower(),
        PlayedStatus = PlayStatus.ToString()
      };

      return entry;
    } // ToDatabaseEntry

    public void Copy(GameListEntry entry)
    {
      Name = entry.Name;
      IsOnPS4 = entry.IsOnPS4;
      IsOnPS3 = entry.IsOnPS3;
      IsOnPSVita = entry.IsOnPSVita;
      IsOnPC = entry.IsOnPC;
      Owned = entry.Owned;
      PlayStatus = entry.PlayStatus;
      DateAdded = entry.DateAdded;
    }

    #endregion // Public Methods
  }
}
