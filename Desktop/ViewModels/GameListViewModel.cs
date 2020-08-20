using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop.ViewModels
{
  public class GameListViewModel : ViewModelBase
  {
    #region Variables

    private IGameListModel model;

    #endregion // Variables

    #region Construction

    public GameListViewModel(IGameListModel model)
    {
      this.model = model;

      AddGameCommand = new RelayCommand(param => this.AddGame());
      EditGameCommand = new RelayCommand(param => this.EditGame());
      DeleteGameCommand = new RelayCommand(param => this.DeleteGame());

      UpdateGameList();
    }

    #endregion // Construction

    #region Properties

    private ObservableCollection<GameListEntryViewModel> _gameCollection = new ObservableCollection<GameListEntryViewModel>();
    /// <summary>
    /// Get/Set the list of games
    /// </summary>
    public ObservableCollection<GameListEntryViewModel> GameCollection
    {
      get
      {
        return _gameCollection;
      }
      private set
      {
        _gameCollection = value;
        RaisePropertyChanged("GameList");
        RaisePropertyChanged("GameCollection");
      }
    }

    private GameListEntryViewModel _selectedEntry = null;
    /// <summary>
    /// Get the selected entry from the list
    /// </summary>
    public GameListEntryViewModel SelectedEntry { get { return _selectedEntry; } set { _selectedEntry = value; } }

    private string _searchText = String.Empty;
    /// <summary>
    /// Holds the search text the to filter game names
    /// </summary>
    public string SearchText { get { return _searchText; } set { _searchText = value; UpdateGameList(); } }

    #endregion // Properties

    #region Commands

    /// <summary>
    /// Add a new game to the list
    /// </summary>
    public ICommand AddGameCommand { get; set; }
    public void AddGame()
    {
      model.AddGame();
      UpdateGameList();
    }

    /// <summary>
    /// Edit the currently selected item
    /// </summary>
    public ICommand EditGameCommand { get; set; }
    public void EditGame()
    {
      if (SelectedEntry != null)
      {
        model.EditGame(SelectedEntry.Model);
        UpdateGameList();
      }
    }

    /// <summary>
    /// Delete the currently selected item
    /// </summary>
    public ICommand DeleteGameCommand { get; set; }
    public void DeleteGame()
    {
      if (SelectedEntry != null)
      {
        model.DeleteGame(SelectedEntry.Model);
        UpdateGameList();
      }
    }

    #endregion // Commands

    #region Implementation

    private async void UpdateGameList()
    {
      List<GameListEntry> games = await model.GetGameList();
      
      if (!String.IsNullOrEmpty(SearchText))
      {
        games = games.Where(entry => entry.Name.ToLower().Contains(SearchText.ToLower())).ToList();
      }

      if (games != null)
      {
        GameCollection.Clear();
        
        foreach(GameListEntry entry in games)
        {
          GameCollection.Add(new GameListEntryViewModel(entry));
        }
      }
    } // UpdateGameList

    #endregion // Implementation
  }
}
