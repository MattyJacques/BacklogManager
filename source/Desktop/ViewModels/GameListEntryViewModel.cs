using Database.Game.Models;
using Desktop.Models;
using GalaSoft.MvvmLight;
using System;
using System.Linq;

namespace Desktop.ViewModels
{
  public class GameListEntryViewModel : ViewModelBase
  {
    #region Public Constructors

    public GameListEntryViewModel(GameListEntry entry)
    {
      Model = entry;
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Get the date the game was added to the collection from the model
    /// </summary>
    public DateTime DateAdded
    {
      get => Model.DateAdded;
      set => Model.DateAdded = value;
    }

    /// <summary>
    /// Has this entry previously has metadata downloaded
    /// </summary>
    public bool HasDownloadedData
    {
      get => Model.HasDownloadedData;
      set
      {
        Model.HasDownloadedData = value;
        RaisePropertyChanged("HasDownloadedData");
      }
    }

    /// <summary>
    /// Get if the game is playable on PC from the model
    /// </summary>
    public bool IsOnPC
    {
      get => Model.IsOnPC;
      set => Model.IsOnPC = value;
    }

    /// <summary>
    /// Get if the game is playable on PS3 from the model
    /// </summary>
    public bool IsOnPS3
    {
      get => Model.IsOnPS3;
      set => Model.IsOnPS3 = value;
    }

    /// <summary>
    /// Get if the game is playable on PS4 from the model
    /// </summary>
    public bool IsOnPS4
    {
      get => Model.IsOnPS4;
      set => Model.IsOnPS4 = value;
    }

    /// <summary>
    /// Get if the game is playable on PS Vita from the model
    /// </summary>
    public bool IsOnPSVita
    {
      get => Model.IsOnPSVita;
      set => Model.IsOnPSVita = value;
    }

    /// <summary>
    /// Get the model of the entry
    /// </summary>
    public GameListEntry Model { get; }

    /// <summary>
    /// Get the game name from the model
    /// </summary>
    public string Name
    {
      get => Model.Name;
      set => Model.Name = value;
    }

    /// <summary>
    /// Get if the game is currently owned from the model
    /// </summary>
    public bool Owned
    {
      get => Model.Owned;
      set => Model.Owned = value;
    }

    /// <summary>
    /// Get the current played status from the model
    /// </summary>
    public Status PlayStatus
    {
      get => Model.PlayStatus;
      set => Model.PlayStatus = value;
    }

    #endregion Public Properties

    #region Public Methods

    public void UpdateFromIGDB(IGDB.Models.Game igdbGame) => Model.UpdateFromIGDB(igdbGame);

    #endregion Public Methods
  }
}