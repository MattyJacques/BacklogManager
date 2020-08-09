using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop.ViewModels
{
  public class GameListViewModel : ViewModelBase
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

      UpdateGameList();
      GameCollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    }

    #endregion // Construction

    #region Properties

    private ObservableCollection<GameListEntryViewModel> _gameList = new ObservableCollection<GameListEntryViewModel>();
    /// <summary>
    /// Get/Set the list of games
    /// </summary>
    public ObservableCollection<GameListEntryViewModel> GameList
    { 
      get
      {
        return _gameList;
      }
      private set
      {
        _gameList = value;
        RaisePropertyChanged("GameList");
        RaisePropertyChanged("GameCollection");
      }
    }

    private CollectionView _gameCollectionView = null;
    public CollectionView GameCollectionView
    {
      get
      {
        if (_gameCollectionView == null)
        {
          _gameCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(GameList);
        }

        return _gameCollectionView;
      }
    }

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

    #region Implementation

    private async void UpdateGameList()
    {
      List<GameListEntry> games = await Model.GetGameList();

      if (games != null)
      {
        GameList.Clear();
        
        foreach(GameListEntry entry in games)
        {
          GameList.Add(new GameListEntryViewModel(entry));
        }
      }
    } // UpdateGameList

    #endregion // Implementation
  }
}
