using Desktop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desktop.Interfaces
{
  public interface IGameListModel
  {
    /// <summary>
    /// Add a new game to the collection
    /// </summary>
    void AddGame();

    /// <summary>
    /// Edit a game entry currently in the collection
    /// </summary>
    void EditGame();

    /// <summary>
    /// Delete a game from the collection
    /// </summary>
    void DeleteGame();

    /// <summary>
    /// Return the current game list
    /// </summary>
    Task<List<GameListEntry>> GetGameList();
  }
}
