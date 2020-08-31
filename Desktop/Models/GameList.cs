using Desktop.Data;
using Desktop.Data.Types;
using Desktop.Interfaces;
using Desktop.ViewModels;
using Desktop.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.Models
{
  internal class GameList : IGameListModel
  {
    #region Private Members

    private readonly GameCollectionDatabase _database = new GameCollectionDatabase();
    private List<GameListEntry> _games;

    #endregion Private Members

    #region Public Methods

    public void AddGame()
    {
      GameListEntry newGame = new GameListEntry();

      // Create and show GameManagement view
      GameManagement gameManagement = new GameManagement(newGame);
      GameManagementViewModel viewModel = new GameManagementViewModel(gameManagement);
      GameManagementWindow window = new GameManagementWindow(viewModel);

      // Result is true if save button is pressed
      if (window.ShowDialog() == true)
      {
        if (_database.AddGame(newGame.ToDatabaseEntry()))
        {
          _games.Add(newGame);
        }
        else
        {
          MessageBox.Show("Failed to add game to database", "Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    } // AddGame

    public void DeleteGame(GameListEntry entry)
    {
      if (_database.DeleteGame(entry.Name))
      {
        _games.Remove(entry);
      }
      else
      {
        MessageBox.Show("Failed to delete game from database", "Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    } // DeleteGame

    public void EditGame(GameListEntry entry)
    {
      // Copy entry in case of user cancel
      GameListEntry newEntry = new GameListEntry(entry);

      // Create and show GameManagement view
      GameManagement gameManagement = new GameManagement(newEntry);
      GameManagementViewModel viewModel = new GameManagementViewModel(gameManagement);
      GameManagementWindow window = new GameManagementWindow(viewModel);

      // Result is true if save button is pressed
      if (window.ShowDialog() == true)
      {
        if (_database.EditGame(entry.Name, newEntry.ToDatabaseEntry()))
        {
          entry.Copy(newEntry);
        }
        else
        {
          MessageBox.Show("Failed to update game in database", "Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    } // EditGame

    public async Task<List<GameListEntry>> GetGameList()
    {
      if (_games == null)
      {
        _games = new List<GameListEntry>();

        List<GameDatabaseEntry> databaseList = await _database.GetAllGames();

        foreach (GameDatabaseEntry databaseEntry in databaseList)
        {
          _games.Add(new GameListEntry(databaseEntry));
        }
      }

      return _games;
    }

    #endregion Public Methods

    // GetGameList
  }
}