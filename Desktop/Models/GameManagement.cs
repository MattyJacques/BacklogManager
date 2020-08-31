using Desktop.Data.Types;
using Desktop.Interfaces;

namespace Desktop.Models
{
  public class GameManagement : IGameManagementModel
  {
    #region Members

    private readonly GameListEntry _game;

    #endregion // Members

    #region Construction

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

    } // Constructor

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

    #endregion // Properties

    #region IGameManagementModel Implementation

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
    } // SaveGame

    #endregion // IGameManagementModel Implementation
  }
}
