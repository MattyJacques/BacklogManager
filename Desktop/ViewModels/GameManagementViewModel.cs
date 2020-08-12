using Desktop.Data.Types;
using Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
  public class GameManagementViewModel
  {
    #region Properties

    /// <summary>
    /// Get the model of the entry
    /// </summary>
    public GameManagement Model { get; }

    /// <summary>
    /// Get/Set the game name from the model
    /// </summary>
    public String Name { get { return Model.Name; } set { Model.Name = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PS4 from the model
    /// </summary>
    public bool IsOnPS4 { get { return Model.IsOnPS4; } set { Model.IsOnPS4 = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PS3 from the model
    /// </summary>
    public bool IsOnPS3 { get { return Model.IsOnPS3; } set { Model.IsOnPS3 = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PS Vita from the model
    /// </summary>
    public bool IsOnPSVita { get { return Model.IsOnPSVita; } set { Model.IsOnPSVita = value; } }

    /// <summary>
    /// Get/Set if the game is playable on PC from the model
    /// </summary>
    public bool IsOnPC { get { return Model.IsOnPC; } set { Model.IsOnPC = value; } }

    /// <summary>
    /// Get/Set the current played status from the model
    /// </summary>
    public Status PlayStatus { get { return Model.PlayStatus; } set { Model.PlayStatus = value; } }

    /// <summary>
    /// Get/Set if the game is currently owned from the model
    /// </summary>
    public bool Owned { get { return Model.Owned; } set { Model.Owned = value; } }

    #endregion // Properties

    #region Construction

    public GameManagementViewModel(GameManagement model)
    {
      Model = model;
    } // Constructor

    #endregion // Construction
  }
}
