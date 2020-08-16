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
    #region Members

    private GameManagement model { get; }

    #endregion // Members

    #region Construction

    public GameManagementViewModel(GameManagement model)
    {
      this.model = model;

      SaveGameCommand = new RelayCommand(param => this.SaveGame());
      CancelCommand = new RelayCommand(param => this.Cancel());

    } // Constructor

    #endregion // Construction

    #region Properties

    private bool? _dialogResult;
    /// <summary>
    /// Get the bool result of the dialog
    /// </summary>
    public bool? DialogResult { get { return _dialogResult; } set { _dialogResult = value; } }

    /// <summary>
    /// Get/Set the game name from the model
    /// </summary>
    public string Name { get { return model.Name; } set { model.Name = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PS4 from the model
    /// </summary>
    public bool IsOnPS4 { get { return model.IsOnPS4; } set { model.IsOnPS4 = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PS3 from the model
    /// </summary>
    public bool IsOnPS3 { get { return model.IsOnPS3; } set { model.IsOnPS3 = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PS Vita from the model
    /// </summary>
    public bool IsOnPSVita { get { return model.IsOnPSVita; } set { model.IsOnPSVita = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PC from the model
    /// </summary>
    public bool IsOnPC { get { return model.IsOnPC; } set { model.IsOnPC = value; } }

    /// <summary>
    /// Get/Set the current played status from the model
    /// </summary>
    public Status PlayStatus { get { return model.PlayStatus; } set { model.PlayStatus = value; } }

    /// <summary>
    /// Get/Set if the game is currently owned from the model
    /// </summary>
    public bool Owned { get { return model.Owned; } set { model.Owned = value; } }

    /// <summary>
    /// Stored action to close the attached view
    /// </summary>
    public Action CloseAction { get; set; }

    #endregion // Properties

    #region Commands

    /// <summary>
    /// Save the game using the data current in the view
    /// </summary>
    public ICommand SaveGameCommand { get; set; }
    public void SaveGame()
    {
      model.SaveGame();
      DialogResult = true;
      RaisePropertyChanged("DialogResult");

      CloseAction();
    }

    /// <summary>
    /// Cancel the dialog without saving the data
    /// </summary>
    public ICommand CancelCommand { get; set; }
    public void Cancel() => CloseAction();

    #endregion // Commands
  }
}
