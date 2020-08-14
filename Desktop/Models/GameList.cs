using Desktop.Data;
using Desktop.Data.Types;
using Desktop.Interfaces;
using Desktop.ViewModels;
using Desktop.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desktop.Models
{
  class GameList : IGameListModel
  {
    #region Members

    GameCollectionDatabase database = new GameCollectionDatabase();
    List<GameListEntry> games;

    #endregion // Members

    #region IGameListModel Implementation

    public void AddGame()
    {
      GameListEntry newGame = new GameListEntry();

      // Create and show GameManagement view
      GameManagement gameManagement = new GameManagement(newGame);
      GameManagementViewModel viewModel = new GameManagementViewModel(gameManagement);
      GameManagementWindow window = new GameManagementWindow(viewModel);

      if (window.ShowDialog() == true)
      {
        games.Add(newGame);
        database.AddGame(newGame.ToDatabaseEntry());
      }
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
      if (games == null)
      {
        games = new List<GameListEntry>();

        List<GameDatabaseEntry> databaseList = await database.GetAllGames();

        foreach (GameDatabaseEntry databaseEntry in databaseList)
        {
          games.Add(new GameListEntry(databaseEntry));
        }
      }

      return games;
    } // GetGameList

    #endregion // IGameListModel Implementation
  }
}
