using Desktop.Models;
using Desktop.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.Interfaces
{
  public interface IMetadataDownloadModel
  {
    #region Public Methods

    /// <summary>
    /// Download all metadata for games that do not already have metadata
    /// </summary>
    /// <param name="games">Collection of games to download metadata for</param>
    Task<ObservableCollection<GameListEntryViewModel>> IGDBGetAllGames(ObservableCollection<GameListEntryViewModel> games);

    /// <summary>
    /// Search for a single game on IGDB
    /// </summary>
    /// <param name="name">Name of the game</param>
    Task<ObservableCollection<IGDB.Models.Game>> IGDBSearchGame(string name);

    #endregion Public Methods
  }
}