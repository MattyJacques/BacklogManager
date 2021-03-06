﻿using Database.Game.Models;
using Desktop.Data.Types;
using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;

namespace Desktop.ViewModels
{
  public class GameManagementViewModel : ViewModelBase
  {
    #region Private Members

    private readonly GameListEntry _entry;
    private readonly IGameManagementModel _model;
    private bool? _dialogResult;

    #endregion Private Members

    #region Public Constructors

    public GameManagementViewModel(IGameManagementModel model)
    {
      _model = model;
      _entry = _model.GetEntry();

      SaveGameCommand = new RelayCommand(param => SaveGame());
      CancelCommand = new RelayCommand(param => Cancel());
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Cancel the dialog without saving the data
    /// </summary>
    public ICommand CancelCommand { get; set; }

    /// <summary>
    /// Stored action to close the attached view
    /// </summary>
    public Action CloseAction { get; set; }

    /// <summary>
    /// Get the bool result of the dialog
    /// </summary>
    public bool? DialogResult { get => _dialogResult; set => _dialogResult = value; }

    /// <summary>
    /// Get/Set if the game is playable on PC from the model
    /// </summary>
    public bool IsOnPC { get => _entry.IsOnPC; set => _entry.IsOnPC = value; }

    /// <summary>
    /// Get/Set if the game is playable on PS3 from the model
    /// </summary>
    public bool IsOnPS3 { get => _entry.IsOnPS3; set => _entry.IsOnPS3 = value; }

    /// <summary>
    /// Get/Set if the game is playable on PS4 from the model
    /// </summary>
    public bool IsOnPS4 { get => _entry.IsOnPS4; set => _entry.IsOnPS4 = value; }

    /// <summary>
    /// Get/Set if the game is playable on PS Vita from the model
    /// </summary>
    public bool IsOnPSVita { get => _entry.IsOnPSVita; set => _entry.IsOnPSVita = value; }

    /// <summary>
    /// Get/Set the game name from the model
    /// </summary>
    public string Name { get => _entry.Name; set => _entry.Name = value; }

    /// <summary>
    /// Get/Set if the game is currently owned from the model
    /// </summary>
    public bool Owned { get => _entry.Owned; set => _entry.Owned = value; }

    /// <summary>
    /// Get/Set the current played status from the model
    /// </summary>
    public Status PlayStatus { get => _entry.PlayStatus; set => _entry.PlayStatus = value; }

    /// <summary>
    /// Save the game using the data current in the view
    /// </summary>
    public ICommand SaveGameCommand { get; set; }

    #endregion Public Properties

    #region Public Methods

    public void Cancel()
    {
      CloseAction();
    }

    public void SaveGame()
    {
      _model.SaveGame(_entry);
      DialogResult = true;
      RaisePropertyChanged("DialogResult");

      CloseAction();
    }

    #endregion Public Methods
  }
}