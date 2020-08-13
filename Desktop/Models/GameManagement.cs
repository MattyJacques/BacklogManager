using Desktop.Data.Types;
using Desktop.Interfaces;
using System;
using System.Windows.Input;

namespace Desktop.Models
{
  public class GameManagement : IGameManagementModel
  {
    #region Members

    private GameListEntry GameEntry;

    #endregion // Members

    #region Construction

    public GameManagement(GameListEntry game)
    {
      GameEntry = game;

      Name = game.Name;
      IsOnPS4 = game.IsOnPS4;
      IsOnPS3 = game.IsOnPS3;
      IsOnPSVita = game.IsOnPSVita;
      IsOnPC = game.IsOnPC;
      Owned = game.Owned;
      PlayStatus = game.PlayStatus;

    } // Constructor

    #endregion // Construction

    #region Properties

    private String _name = String.Empty;
    /// <summary>
    /// Get/set the title of the game
    /// </summary>
    public String Name { get { return _name; } set { _name = value; } }

    private bool _isOnPS4 = false;
    /// <summary>
    /// Get/set if the game is playable on PS4
    /// </summary>
    public bool IsOnPS4 { get { return _isOnPS4; } set { _isOnPS4 = value; } }

    private bool _isOnPS3 = false;
    /// <summary>
    /// Get/set if the game is playable on PS3
    /// </summary>
    public bool IsOnPS3 { get { return _isOnPS3; } set { _isOnPS3 = value; } }

    private bool _isOnPSVita = false;
    /// <summary>
    /// Get/set if the game is playable on PS Vita
    /// </summary>
    public bool IsOnPSVita { get { return _isOnPSVita; } set { _isOnPSVita = value; } }

    private bool _isOnPC = false;
    /// <summary>
    /// Get/set if the game is playable on PC
    /// </summary>
    public bool IsOnPC { get { return _isOnPC; } set { _isOnPC = value; } }

    private Status _playStatus = Status.NotPlayed;
    /// <summary>
    /// Get/set the play status of the game
    /// </summary>
    public Status PlayStatus { get { return _playStatus; } set { _playStatus = value; } }

    private bool _owned = false;
    /// <summary>
    /// Get/set the if the game is owned
    /// </summary>
    public bool Owned { get { return _owned; } set { _owned = value; } }

    #endregion // Properties

    #region IGameManagementModel Implementation

    /// <summary>
    /// Save the data that is currently set
    /// </summary>
    public void SaveGame()
    {
      GameEntry.Name = Name;
      GameEntry.IsOnPS4 = IsOnPS4;
      GameEntry.IsOnPS3 = IsOnPS3;
      GameEntry.IsOnPSVita = IsOnPSVita;
      GameEntry.IsOnPC = IsOnPC;
      GameEntry.Owned = Owned;
      GameEntry.PlayStatus = PlayStatus;
    } // SaveGame

    #endregion // IGameManagementModel Implementation
  }
}
