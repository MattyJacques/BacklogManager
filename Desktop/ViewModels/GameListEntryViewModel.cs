using Desktop.Data.Types;
using Desktop.Models;
using System;

namespace Desktop.ViewModels
{
  public class GameListEntryViewModel
  {
    #region Constructor

    public GameListEntryViewModel(GameListEntry entry)
    {
      Model = entry;
    } // Constructor

    #endregion // Constructor

    #region Properties

    /// <summary>
    /// Get the model of the entry
    /// </summary>
    public GameListEntry Model { get; }

    /// <summary>
    /// Get the game name from the model
    /// </summary>
    public string Name => Model.Name;

    /// <summary>
    /// Get if the game is playable on PS4 from the model
    /// </summary>
    public bool IsOnPS4 => Model.IsOnPS4;

    /// <summary>
    /// Get if the game is playable on PS3 from the model
    /// </summary>
    public bool IsOnPS3 => Model.IsOnPS3;

    /// <summary>
    /// Get if the game is playable on PS Vita from the model
    /// </summary>
    public bool IsOnPSVita => Model.IsOnPSVita;

    /// <summary>
    /// Get if the game is playable on PC from the model
    /// </summary>
    public bool IsOnPC => Model.IsOnPC;

    /// <summary>
    /// Get the current played status from the model
    /// </summary>
    public Status PlayStatus => Model.PlayStatus;

    /// <summary>
    /// Get if the game is currently owned from the model
    /// </summary>
    public bool Owned => Model.Owned;

    /// <summary>
    /// Get the date the game was added to the collection from the model
    /// </summary>
    public DateTime DateAdded => Model.DateAdded;

    #endregion // Properties
  }
}
