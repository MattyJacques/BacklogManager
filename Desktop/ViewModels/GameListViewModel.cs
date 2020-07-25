using DesktopApplication.Helpers;
using DesktopApplication.Interfaces;
using DesktopApplication.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApplication.ViewModels
{
  class GameListViewModel
  {
    #region Variables

    private IGameListModel Model;

    #endregion // Variables

    #region Construction

    public GameListViewModel(IGameListModel model)
    {
      Model = model;

      AddGameCommand = new RelayCommand(param => this.AddGame());
      EditGameCommand = new RelayCommand(param => this.EditGame());
      DeleteGameCommand = new RelayCommand(param => this.DeleteGame());
    }

    #endregion // Construction

    #region Properties

    private ObservableCollection<GameListEntry> _gameList;
    /// <summary>
    /// Get/Set the list of games
    /// </summary>
    public ObservableCollection<GameListEntry> GameList { get { return _gameList; } private set { _gameList = value; } }

    #endregion // Properties

    #region Commands

    /// <summary>
    /// Add a new game to the list
    /// </summary>
    public ICommand AddGameCommand { get; set; }
    public void AddGame() => Model.AddGame();

    /// <summary>
    /// Edit the currently selected item
    /// </summary>
    public ICommand EditGameCommand { get; set; }
    public void EditGame() => Model.EditGame();

    /// <summary>
    /// Delete the currently selected item
    /// </summary>
    public ICommand DeleteGameCommand { get; set; }
    public void DeleteGame() => Model.DeleteGame();

    #endregion // Commands
  }
}
