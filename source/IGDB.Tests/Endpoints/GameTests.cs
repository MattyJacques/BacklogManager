using IGDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IGDB.Tests.Endpoints
{
  [TestClass]
  public class GameTests
  {
    #region Private Members

    private IGDBApi _api = IGDB.Client.Create();

    #endregion Private Members

    #region Public Methods

    [TestMethod]
    public async Task ShouldReturnResponseWithoutQuery()
    {
      var games = await _api.QueryAsync<Game>(Client.Endpoints.Games);

      Assert.IsNotNull(games);
      Assert.IsTrue(games.Length == 10);
    }

    [TestMethod]
    public async Task ShouldReturnResponseWithSingleGame()
    {
      var games = await _api.QueryAsync<Game>(Client.Endpoints.Games, "fields collection.id, cover.id, expansions.name, first_release_date, genres.id, id, name, platforms.id, time_to_beat.completely, total_rating; where name = \"The Last of Us\";");

      Assert.IsNotNull(games);
      Assert.IsTrue(games.Length == 1);

      var game = games[0];

      Assert.AreEqual(864, game.Collection.Id);
      Assert.AreEqual(81915, game.Cover.Id);
      Assert.AreEqual("The Last of Us: Left Behind", game.Expansions.First().Name);
      Assert.AreEqual(new DateTime(2013, 06, 14, 0, 0, 0, DateTimeKind.Utc), game.FirstReleaseDate);
      Assert.AreEqual(5, game.Genres[0].Id);
      Assert.AreEqual(31, game.Genres[1].Id);
      Assert.AreEqual(1009, game.Id);
      Assert.AreEqual("The Last of Us", game.Name);
      Assert.AreEqual(9, game.Platforms.First().Id);
      Assert.IsTrue((game.TotalRating - 93) < 1);
    }

    [TestMethod]
    public async Task ShouldReturnResponseWithSingleGameExpandedCover()
    {
      var games = await _api.QueryAsync<Game>(Client.Endpoints.Games, "fields id,cover.*; where id = 4;");

      Assert.IsNotNull(games);

      var game = games[0];

      Assert.IsNotNull(game.Cover);
      Assert.AreEqual(756, game.Cover.Width);
    }

    [TestMethod]
    public async Task ShouldReturnResponseWithSingleGameExpandedGenres()
    {
      var games = await _api.QueryAsync<Game>(Client.Endpoints.Games, "fields id,name,genres.name; where id = 4;");

      Assert.IsNotNull(games);

      var game = games[0];

      Assert.AreEqual("Thief", game.Name);
      Assert.IsTrue(game.Genres.Count > 0);
      Assert.AreEqual("Shooter", game.Genres[0].Name);
    }

    #endregion Public Methods
  }
}