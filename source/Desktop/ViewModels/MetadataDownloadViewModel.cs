using Database.Game;
using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using Desktop.Properties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop.ViewModels
{
  public class MetadataDownloadViewModel : ViewModelBase, IPageViewModel
  {
    #region Private Members

    private readonly GameDatabase _database = new GameDatabase();
    private readonly IGameListModel _gameListModel;
    private readonly IMetadataDownloadModel _metadataModel;

    private ObservableCollection<GameListEntryViewModel> _gameCollection =
      new ObservableCollection<GameListEntryViewModel>();

    private CollectionView _gameCollectionView;
    private string _name = "Metadata Download";
    private ObservableCollection<IGDB.Models.Game> _searchResults;
    private GameListEntryViewModel _selectedEntry = null;
    private IGDB.Models.Game _selectedResult = null;

    #endregion Private Members

    #region Public Constructors

    public MetadataDownloadViewModel(IGameListModel gameListModel,
                                     IMetadataDownloadModel metadataModel)
    {
      _gameListModel = gameListModel;
      _metadataModel = metadataModel;

      AutoSearchCommand =
        new RelayCommand(param => UpdateAllGames());

      UpdateGameList();
      GameCollectionView.SortDescriptions.Add(new SortDescription("Name",
                                                                  ListSortDirection.Ascending));

      MessengerInstance.Register<NotificationMessage>(this, HandleNotification);
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Automatically try and match every game that has not already been matched
    /// </summary>
    public ICommand AutoSearchCommand { get; set; }

    /// <summary>
    /// Get/Set the list of games
    /// </summary>
    public ObservableCollection<GameListEntryViewModel> GameCollection
    {
      get => _gameCollection;
      private set => _gameCollection = value;
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
          _gameCollectionView =
            (CollectionView)CollectionViewSource.GetDefaultView(GameCollection);
        }

        return _gameCollectionView;
      }
    }

    /// <summary>
    /// The name of the view model
    /// </summary>
    public string Name { get => _name; set => _name = value; }

    /// <summary>
    /// Search results from the metadata provider
    /// </summary>
    public ObservableCollection<IGDB.Models.Game> SearchResults
    {
      get => _searchResults;
      private set => Set(ref _searchResults, value);
    }

    /// <summary>
    /// Get the selected entry from the list
    /// </summary>
    public GameListEntryViewModel SelectedEntry
    {
      get => _selectedEntry;
      set
      {
        _selectedEntry = value;

        if (value != null)
        {
          SearchGame(_selectedEntry.Name);
        }
      }
    }

    /// <summary>
    /// Search result that the user has selected
    /// </summary>
    public IGDB.Models.Game SelectedSearchResult
    {
      get => _selectedResult;
      set
      {
        _selectedResult = value;

        if (_selectedResult != null)
        {
          SelectedEntry.UpdateFromIGDB(_selectedResult);
          _database.EditGame(SelectedEntry.Name, SelectedEntry.Model.ToDatabaseEntry());
        }
      }
    }

    #endregion Public Properties

    #region Private Methods

    /// <summary>
    /// Handle notifications, action depends on the message
    /// </summary>
    /// <param name="message">Type of notification</param>
    private void HandleNotification(NotificationMessage message)
    {
      if (message.Notification.Equals(Resources.NotificationUpdateGameList))
      {
        UpdateGameList();
      }
    }

    /// <summary>
    /// Search the metadata provider for a game with the specified name
    /// </summary>
    /// <param name="name">Name of the game</param>
    private async void SearchGame(string name)
    {
      SearchResults = await _metadataModel.IGDBSearchGame(name);
    }

    /// <summary>
    /// Update the game data with metadata from the provider
    /// </summary>
    private async void UpdateAllGames()
    {
      await _metadataModel.IGDBGetAllGames(_gameCollection);

      // TODO: Implement way to tell which game(s) have changed
      foreach (GameListEntryViewModel game in _gameCollection)
      {
        _database.EditGame(game.Name, game.Model.ToDatabaseEntry());
      }

      MessengerInstance.Send(new NotificationMessage(Resources.NotificationUpdateGameList));
    }

    /// <summary>
    /// Update the game list from the database
    /// </summary>
    private void UpdateGameList()
    {
      List<GameListEntry> games = _gameListModel.GetGameList();

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