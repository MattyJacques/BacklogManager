using Desktop.Data.Types;
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
  public class GameListViewModel : ViewModelBase, IPageViewModel
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

    private string _name = "GameList";
    /// <summary>
    /// The name of the view model
    /// </summary>
    public string Name { get { return _name; } set { _name = value; } }

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

    private bool _showFilters = false;
    /// <summary>
    /// Whether to show the filter GroupBox
    /// </summary>
    public bool ShowFilters { get { return _showFilters; } set { _showFilters = value; RaisePropertyChanged("ShowFilters");  } }

    private bool _showNotPlayed = true;
    /// <summary>
    /// Whether to show not played games
    /// </summary>
    public bool ShowNotPlayed { get { return _showNotPlayed; } set { _showNotPlayed = value; UpdateGameList(); } }

    private bool _showPlayed = true;
    /// <summary>
    /// Whether to show played games
    /// </summary>
    public bool ShowPlayed { get{ return _showPlayed; } set { _showPlayed = value; UpdateGameList(); } }

    private bool _showComplete = false;
    /// <summary>
    /// Whether to show completed games
    /// </summary>
    public bool ShowComplete { get { return _showComplete; } set { _showComplete = value; UpdateGameList(); } }

    private bool _showAbandoned = false;
    /// <summary>
    /// Whether to show abandoned games
    /// </summary>
    public bool ShowAbandoned { get { return _showAbandoned; } set { _showAbandoned = value; UpdateGameList(); } }

    private bool _showPC = true;
    /// <summary>
    /// Whether to show PC games
    /// </summary>
    public bool ShowPC { get { return _showPC; } set { _showPC = value; UpdateGameList(); } }

    private bool _showPS4 = true;
    /// <summary>
    /// Whether to show PS4 games
    /// </summary>
    public bool ShowPS4 { get { return _showPS4; } set { _showPS4 = value; UpdateGameList(); } }

    private bool _showPS3 = true;
    /// <summary>
    /// Whether to show PS3 games
    /// </summary>
    public bool ShowPS3 { get { return _showPS3; } set { _showPS3 = value; UpdateGameList(); } }

    private bool _showPSVita = true;
    /// <summary>
    /// Whether to show vita games
    /// </summary>
    public bool ShowPSVita { get { return _showPSVita; } set { _showPSVita = value; UpdateGameList(); } }

    private bool _showNotOwned = true;
    /// <summary>
    /// Whether to show not owned games
    /// </summary>
    public bool ShowNotOwned { get { return _showNotOwned; } set { _showNotOwned = value; UpdateGameList(); } }

    private bool _showOwned = true;
    /// <summary>
    /// Whether to show owned games
    /// </summary>
    public bool ShowOwned { get { return _showOwned; } set { _showOwned = value; UpdateGameList(); } }

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

      games = games.Where(entry => ((ShowNotPlayed && entry.PlayStatus == Status.NotPlayed) ||
                                   (ShowPlayed && entry.PlayStatus == Status.Played) ||
                                   (ShowComplete && entry.PlayStatus == Status.Complete) ||
                                   (ShowAbandoned && entry.PlayStatus == Status.Abandoned)) &&
                                   ((ShowPC && entry.IsOnPC) ||
                                   (ShowPS4 && entry.IsOnPS4) ||
                                   (ShowPS3 && entry.IsOnPS3) ||
                                   (ShowPSVita && entry.IsOnPSVita)) &&
                                   ((ShowNotOwned && !entry.Owned) ||
                                   (ShowOwned && entry.Owned))).ToList();

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
