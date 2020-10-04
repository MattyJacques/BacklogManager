using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
  public interface IGameDatabase
  {
    #region Public Methods

    /// <summary>
    /// Add a game to the database
    /// </summary>
    /// <param name="entry">Game data to add to the database</param>
    /// <returns>Whether the operation was successful</returns>
    bool AddGame(GameDatabaseEntry entry);

    /// <summary>
    /// Delete a game from the database
    /// </summary>
    /// <param name="gameName">Name of the game to delete</param>
    /// <returns>Whether the operation was successful</returns>
    bool DeleteGame(string gameName);

    /// <summary>
    /// Edit an existing entry in the database
    /// </summary>
    /// <returns>Whether the operation was successful</returns>
    bool EditGame();

    /// <summary>
    /// Get all the game entries from the database
    /// </summary>
    void GetAllGames();

    /// <summary>
    /// Get the amount of entries that have the provided played status on the specified platform
    /// </summary>
    /// <returns></returns>
    int GetAmountWithPlatformStatus();

    #endregion Public Methods
  }
}