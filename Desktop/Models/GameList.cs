using Desktop.Data;
using Desktop.Data.Types;
using Desktop.Interfaces;
using Desktop.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desktop.Models
{
  class GameList : IGameListModel
  {
    #region Members

    GameCollectionDatabase database = new GameCollectionDatabase();

    #endregion // Members

    #region IGameListModel Implementation

    public GameList()
    {
      GameManagementWindow window = new GameManagementWindow();
      window.Show();
    }

    public void AddGame()
    {
      throw new NotImplementedException();
    } // AddGame

    public void DeleteGame()
    {
      throw new NotImplementedException();
    } // DeleteGame

    public void EditGame()
    {
      throw new NotImplementedException();
    } // EditGame

    public async Task<List<GameListEntry>> GetGameList()
    {
      List<GameListEntry> gameList = new List<GameListEntry>();

      List<GameDatabaseEntry> databaseList = await database.GetAllGames();

      foreach (GameDatabaseEntry databaseEntry in databaseList)
      {
        gameList.Add(new GameListEntry(databaseEntry));
      }

      return gameList;
    } // GetGameList

    #endregion // IGameListModel Implementation
  }
}
