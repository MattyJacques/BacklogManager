using Desktop.Data.Types;
using Desktop.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace Desktop.Models
{
  public class GameManagement : IGameManagementModel
  {
    #region Members

    private GameListEntry game;

    #endregion // Members

    #region Construction

    public GameManagement(GameListEntry gameEntry)
    {
      game = gameEntry;

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
      game.Name = Name;
      game.IsOnPS4 = IsOnPS4;
      game.IsOnPS3 = IsOnPS3;
      game.IsOnPSVita = IsOnPSVita;
      game.IsOnPC = IsOnPC;
      game.Owned = Owned;
      game.PlayStatus = PlayStatus;
    } // SaveGame

    #endregion // IGameManagementModel Implementation
  }
}
