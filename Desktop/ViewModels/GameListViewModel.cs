using Desktop.Data;
using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using Desktop.Views;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

      GameManagement gameManagement = new GameManagement(GameCollection.FirstOrDefault().Model);
      GameManagementViewModel viewModel = new GameManagementViewModel(gameManagement);
      GameManagementWindow window = new GameManagementWindow(viewModel);
      window.Show();
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

    private CollectionView _gameCollectionView = null;
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
