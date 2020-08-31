using Desktop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desktop.Interfaces
{
  public interface IGameListModel
  {
    #region Public Methods

    /// <summary>
    /// Add a new game to the collection
    /// </summary>
    void AddGame();

    /// <summary>
    /// Delete a game from the collection
    /// </summary>
    void DeleteGame(GameListEntry entry);

    /// <summary>
    /// Edit a game entry currently in the collection
    /// </summary>
    void EditGame(GameListEntry entry);

    /// <summary>
    /// Return the current game list
    /// </summary>
    Task<List<GameListEntry>> GetGameList();

    #endregion Public Methods
  }
}