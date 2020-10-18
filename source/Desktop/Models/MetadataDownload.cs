using Desktop.Interfaces;
using Desktop.ViewModels;
using IGDB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
  internal class MetadataDownload : IMetadataDownloadModel
  {
    #region Public Methods

    public async Task<ObservableCollection<GameListEntryViewModel>> IGDBGetAllGames(ObservableCollection<GameListEntryViewModel> games)
    {
      List<string> names = new List<string>();

      foreach (GameListEntryViewModel game in games)
      {
        if (!game.HasDownloadedData)
        {
          names.Add(game.Name);
        }
      }

      List<IGDB.Models.Game> results = await IGDB.IGDBProvider.GetMultipleGames(names);

      for (int i = 0; i < games.Count; i++)
      {
        if (results[i] != null &&
            games[i].Name.Equals(results[i].Name, StringComparison.OrdinalIgnoreCase))
        {
          games[i].UpdateFromIGDB(results[i]);
        }
      }

      return games;
    }

    public async Task<ObservableCollection<Game>> IGDBSearchGame(string name)
    {
      return new ObservableCollection<IGDB.Models.Game>(await IGDB.IGDBProvider.SearchGame(name));
    }

    #endregion Public Methods
  }
}