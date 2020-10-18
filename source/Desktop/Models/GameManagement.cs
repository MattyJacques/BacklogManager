using Desktop.Data.Types;
using Desktop.Extensions.Properties;
using Desktop.Interfaces;

namespace Desktop.Models
{
  public class GameManagement : IGameManagementModel
  {
    #region Private Members

    private GameListEntry _oldGameData;

    #endregion Private Members

    #region Public Constructors

    public GameManagement(GameListEntry gameEntry)
    {
      _oldGameData = gameEntry;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Get the GameListEntry that the user is currently editing
    /// </summary>
    /// <returns></returns>
    public GameListEntry GetEntry() => _oldGameData;

    /// <summary>
    /// Save the data that is currently set
    /// </summary>
    public void SaveGame(GameListEntry newData)
    {
      _oldGameData = newData;
    }

    #endregion Public Methods
  }
}