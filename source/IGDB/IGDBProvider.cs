using IGDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGDB
{
  public static class IGDBProvider
  {
    #region Private Members

    private const string _fieldString = "fields *, " +
                                        "collection.name, collection.games.name, " +
                                        "cover.*, " +
                                        "dlcs.name, " +
                                        "expansions.name, " +
                                        "genres.name, " +
                                        "parent_game.*, " +
                                        "platforms.*, platforms.platform_logo.*;";

    private static readonly IGDBApi _api = IGDB.Client.Create();

    #endregion Private Members

    #region Public Methods

    /// <summary>
    /// Get all platforms in IGDB
    /// </summary>
    /// <returns>List of all platforms</returns>
    public async static Task<List<Platform>> GetAllPlatforms()
    {
      return (await _api.QueryAsync<Platform>(Client.Endpoints.Platforms,
                                              "fields *, " +
                                              "product_family.*, " +
                                              "platform_logo.*; " +
                                              "limit 500;")).ToList();
    }

    /// <summary>
    /// Return a Game with the exact name as provided
    /// </summary>
    /// <param name="name">Name of the game to get data for (Must be exact)</param>
    /// <returns>Game object for name provided</returns>
    public async static Task<Game> GetGame(string name)
    {
      return (await _api.QueryAsync<Game>(Client.Endpoints.Games,
                                          $"{_fieldString} where name = \"{name}\"; ")
             ).FirstOrDefault();
    }

    /// <summary>
    /// Return a List of games that contains the IGDB entry for each name
    /// </summary>
    /// <param name="names">List of names to get data for (Must be exact)</param>
    /// <returns>List of entries for each name given</returns>
    public async static Task<List<Game>> GetMultipleGames(List<string> names)
    {
      List<Game> resultsList = new List<Game>();

      foreach (string name in names)
      {
        resultsList.Add(await GetGame(name));
      }

      return resultsList;
    }

    /// <summary>
    /// Search for a game on IGDB using the name provided
    /// </summary>
    /// <param name="name">Name of the game to search</param>
    /// <returns>Returns results of the search in a List Game</returns>
    public async static Task<List<Game>> SearchGame(string name)
    {
      return (await _api.QueryAsync<Game>(Client.Endpoints.Games,
                                          $"search \"{name}\"; {_fieldString}")).ToList();
    }

    /// <summary>
    /// Search for multiple games on IGDB. Each search comes back with a List of Game
    /// </summary>
    /// <param name="names">Names of games to search for</param>
    /// <returns>List of List of Games with search results for each name</returns>
    public async static Task<List<List<Game>>> SearchMultipleGames(List<string> names)
    {
      List<List<Game>> resultsList = new List<List<Game>>();

      foreach (string name in names)
      {
        resultsList.Add(await SearchGame(name));
      }

      return resultsList;
    }

    #endregion Public Methods
  }
}