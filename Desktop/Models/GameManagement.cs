using Desktop.Data.Types;
using Desktop.Interfaces;

namespace Desktop.Models
{
  public class GameManagement : IGameManagementModel
  {
    #region Private Members

    private readonly GameListEntry _game;

    private bool _isOnPC = false;

    private bool _isOnPS3 = false;

    private bool _isOnPS4 = false;

    private bool _isOnPSVita = false;

    private string _name = string.Empty;

    private bool _owned = false;

    private Status _playStatus = Status.NotPlayed;

    #endregion Private Members

    #region Public Constructors

    public GameManagement(GameListEntry gameEntry)
    {
      _game = gameEntry;

      Name = gameEntry.Name;
      IsOnPS4 = gameEntry.IsOnPS4;
      IsOnPS3 = gameEntry.IsOnPS3;
      IsOnPSVita = gameEntry.IsOnPSVita;
      IsOnPC = gameEntry.IsOnPC;
      Owned = gameEntry.Owned;
      PlayStatus = gameEntry.PlayStatus;
    }

    #endregion Public Constructors

    #region Public Properties

    // Constructor
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

    /// <summary>
    /// Save the data that is currently set
    /// </summary>
    public void SaveGame()
    {
      _game.Name = Name;
      _game.IsOnPS4 = IsOnPS4;
      _game.IsOnPS3 = IsOnPS3;
      _game.IsOnPSVita = IsOnPSVita;
      _game.IsOnPC = IsOnPC;
      _game.Owned = Owned;
      _game.PlayStatus = PlayStatus;
    }

    #endregion Public Methods

    // SaveGame
  }
}