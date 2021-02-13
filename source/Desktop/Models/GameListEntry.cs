using Database.Game.Models;
using System;
using System.Linq;
using System.Windows;

namespace Desktop.Models
{
  public class GameListEntry
  {
    #region Private Members

    private GameCollection _collection = new GameCollection();
    private DateTime _dateAdded = DateTime.Today;
    private bool _hasDownloadedData = false;
    private bool _isOnPC = false;
    private bool _isOnPS3 = false;
    private bool _isOnPS4 = false;
    private bool _isOnPSVita = false;
    private string _name = string.Empty;
    private bool _owned = false;
    private Status _playStatus = Status.NotPlayed;

    #endregion Private Members

    /*private Cover Cover

    private List<GameName> Dlcs

    private List<GameName> Expansions

    private DateTimeOffset? FirstReleaseDate

    private List<Genre> Genres

    private List<Platform> Platforms

    private double? TotalRating*/

    #region Public Constructors

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
        HasDownloadedData = databaseEntry.DownloadedData == "true";

        string statusNoSpace = databaseEntry.PlayedStatus.Replace(" ", "");
        PlayStatus = (Status)Enum.Parse(typeof(Status), statusNoSpace, true);

        DateAdded = DateTime.Parse(databaseEntry.AddedDate);
      }
      catch (ArgumentException exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    public GameListEntry(GameListEntry entry)
    {
      Copy(entry);
    }

    public GameListEntry()
    {
    }

    #endregion Public Constructors

    #region Public Properties

    public GameCollection Collection { get => _collection; set => _collection = value; }

    /// <summary>
    /// Get/set the date the game was added to the database
    /// </summary>
    public DateTime DateAdded { get => _dateAdded; set => _dateAdded = value; }

    /// <summary>
    /// Has this entry previously has metadata downloaded
    /// </summary>
    public bool HasDownloadedData { get => _hasDownloadedData; set => _hasDownloadedData = value; }

    /// <summary>
    /// Get/set if the game is playable on PC
    /// </summary>
    public bool IsOnPC { get => _isOnPC; set => _isOnPC = value; }

    /// <summary>
    /// Get/set if the game is playable on PS3
    /// </summary>
    public bool IsOnPS3 { get => _isOnPS3; set => _isOnPS3 = value; }

    /// <summary>
    /// Get/set if the game is playable on PS4
    /// </summary>
    public bool IsOnPS4 { get => _isOnPS4; set => _isOnPS4 = value; }

    /// <summary>
    /// Get/set if the game is playable on PS Vita
    /// </summary>
    public bool IsOnPSVita { get => _isOnPSVita; set => _isOnPSVita = value; }

    /// <summary>
    /// Get/set the title of the game
    /// </summary>
    public string Name { get => _name; set => _name = value; }

    /// <summary>
    /// Get/set the if the game is owned
    /// </summary>
    public bool Owned { get => _owned; set => _owned = value; }

    /// <summary>
    /// Get/set the play status of the game
    /// </summary>
    public Status PlayStatus { get => _playStatus; set => _playStatus = value; }

    #endregion Public Properties

    #region Public Methods

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
      HasDownloadedData = entry.HasDownloadedData;
    }

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
        PlayedStatus = PlayStatus.ToString(),
        DownloadedData = HasDownloadedData.ToString().ToLower()
      };

      return entry;
    }

    public void UpdateFromIGDB(IGDB.Models.Game igdbGame)
    {
      HasDownloadedData = true;
      Name = igdbGame.Name;
      IsOnPS4 = igdbGame.Platforms.Where(e => e.Name == "PlayStation 4").FirstOrDefault() != null;
      IsOnPS3 = igdbGame.Platforms.Where(e => e.Name == "PlayStation 3").FirstOrDefault() != null;
      IsOnPSVita =
        igdbGame.Platforms.Where(e => e.Name == "PlayStation Vita").FirstOrDefault() != null;
      IsOnPC =
        igdbGame.Platforms.Where(e => e.Name == "PC(Microsoft Windows)").FirstOrDefault() != null;

      /*  Category? Category
          Collection Collection
          Cover Cover
          List<GameName> Dlcs
          List<GameName> Expansions
          DateTimeOffset? FirstReleaseDate
          List<Genre> Genres
          List<Platform> Platforms
          double? TotalRating */
    }

    #endregion Public Methods
  }
}