using Desktop.Models;

namespace Desktop.Interfaces
{
  public interface IGameManagementModel
  {
    #region Public Methods

    /// <summary>
    /// Get the GameListEntry that the user is currently managing
    /// </summary>
    GameListEntry GetEntry();

    /// <summary>
    /// Save the data currently set
    /// </summary>
    void SaveGame(GameListEntry newData);

    #endregion Public Methods
  }
}