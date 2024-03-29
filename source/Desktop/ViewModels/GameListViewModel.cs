﻿using Database.Game.Models;
using Desktop.Data.Types;
using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using Desktop.Properties;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop.ViewModels
{
  public class GameListViewModel : ViewModelBase, IPageViewModel
  {
    #region Private Members

    private readonly IGameListModel _model;

    private ObservableCollection<GameListEntryViewModel> _gameCollection = new ObservableCollection<GameListEntryViewModel>();

    private CollectionView _gameCollectionView = null;
    private string _name = "Games";

    private GameListEntryViewModel _nextGameEntry = null;
    private string _searchText = string.Empty;

    private GameListEntryViewModel _selectedEntry = null;
    private bool _showAbandoned = false;

    private bool _showComplete = false;

    private bool _showFilters = false;

    private bool _showNotOwned = true;

    private bool _showNotPlayed = true;

    private bool _showOwned = true;

    private bool _showPC = true;

    private bool _showPlayed = true;

    private bool _showPS3 = true;

    private bool _showPS4 = true;

    private bool _showPSVita = true;

    #endregion Private Members

    #region Public Constructors

    public GameListViewModel(IGameListModel model)
    {
      _model = model;

      AddGameCommand = new RelayCommand(param => AddGame());
      AddFromIGDBCommand = new RelayCommand(param => AddFromIGDB());
      EditGameCommand = new RelayCommand(param => EditGame());
      DeleteGameCommand = new RelayCommand(param => DeleteGame());
      ChooseNextGameCommand = new RelayCommand(param => ChooseNextGame());

      UpdateGameList();
      GameCollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

      LoadNextGame();
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Add a new game to the list using IGDB metadata
    /// </summary>
    public ICommand AddFromIGDBCommand { get; set; }

    /// <summary>
    /// Add a new game to the list
    /// </summary>
    public ICommand AddGameCommand { get; set; }

    /// <summary>
    /// Choose a random next game from the list
    /// </summary>
    public ICommand ChooseNextGameCommand { get; set; }

    /// <summary>
    /// Delete the currently selected item
    /// </summary>
    public ICommand DeleteGameCommand { get; set; }

    /// <summary>
    /// Edit the currently selected item
    /// </summary>
    public ICommand EditGameCommand { get; set; }

    /// <summary>
    /// Get/Set the list of games
    /// </summary>
    public ObservableCollection<GameListEntryViewModel> GameCollection
    {
      get => _gameCollection;
      private set
      {
        _gameCollection = value;
        RaisePropertyChanged("GameList");
        RaisePropertyChanged("GameCollection");
      }
    }

    /// <summary>
    /// Get/Set the collection view, used for sorting
    /// </summary>
    public CollectionView GameCollectionView
    {
      get
      {
        if (_gameCollectionView == null)
        {
          _gameCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(GameCollection);
        }

        return _gameCollectionView;
      }
    }

    /// <summary>
    /// The name of the view model
    /// </summary>
    public string Name { get => _name; set => _name = value; }

    /// <summary>
    /// Get the next game to play chosen at random
    /// </summary>
    public GameListEntryViewModel NextGameEntry { get => _nextGameEntry; set => _nextGameEntry = value; }

    /// <summary>
    /// Holds the search text the to filter game names
    /// </summary>
    public string SearchText { get => _searchText; set { _searchText = value; UpdateGameList(); } }

    /// <summary>
    /// Get the selected entry from the list
    /// </summary>
    public GameListEntryViewModel SelectedEntry { get => _selectedEntry; set => _selectedEntry = value; }

    /// <summary>
    /// Whether to show abandoned games
    /// </summary>
    public bool ShowAbandoned { get => _showAbandoned; set { _showAbandoned = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show completed games
    /// </summary>
    public bool ShowComplete { get => _showComplete; set { _showComplete = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show the filter GroupBox
    /// </summary>
    public bool ShowFilters { get => _showFilters; set { _showFilters = value; RaisePropertyChanged("ShowFilters"); } }

    /// <summary>
    /// Whether to show not owned games
    /// </summary>
    public bool ShowNotOwned { get => _showNotOwned; set { _showNotOwned = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show not played games
    /// </summary>
    public bool ShowNotPlayed { get => _showNotPlayed; set { _showNotPlayed = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show owned games
    /// </summary>
    public bool ShowOwned { get => _showOwned; set { _showOwned = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show PC games
    /// </summary>
    public bool ShowPC { get => _showPC; set { _showPC = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show played games
    /// </summary>
    public bool ShowPlayed { get => _showPlayed; set { _showPlayed = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show PS3 games
    /// </summary>
    public bool ShowPS3 { get => _showPS3; set { _showPS3 = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show PS4 games
    /// </summary>
    public bool ShowPS4 { get => _showPS4; set { _showPS4 = value; UpdateGameList(); } }

    /// <summary>
    /// Whether to show vita games
    /// </summary>
    public bool ShowPSVita { get => _showPSVita; set { _showPSVita = value; UpdateGameList(); } }

    #endregion Public Properties

    #region Public Methods

    public void AddFromIGDB()
    {
      _model.AddFromIGDB();
      UpdateGameList();
    }

    public void AddGame()
    {
      _model.AddGame();
      UpdateGameList();
    }

    public void ChooseNextGame()
    {
      Random nextIndexGen = new Random();
      NextGameEntry = GameCollection[nextIndexGen.Next(GameCollection.Count)];
      RaisePropertyChanged("NextGameEntry");

      Settings.Default.NextGameName = NextGameEntry.Name;
      Settings.Default.Save();
    }

    public void DeleteGame()
    {
      if (SelectedEntry != null)
      {
        _model.DeleteGame(SelectedEntry.Model);
        UpdateGameList();
      }
    }

    public void EditGame()
    {
      if (SelectedEntry != null)
      {
        _model.EditGame(SelectedEntry.Model);
        UpdateGameList();
      }
    }

    #endregion Public Methods

    #region Private Methods

    private void LoadNextGame()
    {
      if (!String.IsNullOrEmpty(Settings.Default.NextGameName))
      {
        try
        {
          NextGameEntry = GameCollection.Where(entry => entry.Name == Settings.Default.NextGameName).First();
        }
        catch (Exception) { } // We choose another game below so nothing to do here
      }

      // This is the first time or something happened to the saved game, choose another
      if (NextGameEntry == null && GameCollection.Count > 0)
      {
        ChooseNextGame();
      }
    }

    private void UpdateGameList()
    {
      List<GameListEntry> games = _model.GetGameList();

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

      if (!string.IsNullOrEmpty(SearchText))
      {
        games = games.Where(entry => entry.Name.ToLower().Contains(SearchText.ToLower())).ToList();
      }

      if (games != null)
      {
        GameCollection.Clear();

        foreach (GameListEntry entry in games)
        {
          GameCollection.Add(new GameListEntryViewModel(entry));
        }
      }
    }

    #endregion Private Methods
  }
}