using IGDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGDB.Tests.Wrappers
{
  [TestClass]
  public class IGDBProviderTests
  {
    #region Private Members

    private readonly Game _remasteredData = new Game()
    {
      Category = Category.MainGame,
      Collection = new Collection() { Name = "The Last of Us" },
      Cover = new Cover() { Id = 70754 },
      Dlcs = new List<GameName>() { new GameName()
      {
        Name = "The Last Of Us Remastered: Agility Survival Skill"
      } },
      Expansions = null,
      FirstReleaseDate = new DateTime(2014, 07, 29, 0, 0, 0, DateTimeKind.Utc),
      Genres = new List<Genre>()
      {
        new Genre() { Name = "Shooter" },
        new Genre() { Name = "Adventure" }
      },
      Id = 6036,
      Name = "The Last of Us Remastered",
      ParentGame = null,
      Platforms = new List<Platform>() { new Platform() { Name = "PlayStation 4" } },
      TotalRating = 95
    };

    private readonly Game _theLastOfUsData = new Game()
    {
      Category = Category.MainGame,
      Collection = new Collection() { Name = "The Last of Us" },
      Cover = new Cover() { Id = 81915 },
      Dlcs = null,
      Expansions = new List<GameName>() { new GameName() { Name = "The Last of Us: Left Behind" } },
      FirstReleaseDate = new DateTime(2013, 06, 14, 0, 0, 0, DateTimeKind.Utc),
      Genres = new List<Genre>()
      {
        new Genre() { Name = "Shooter" },
        new Genre() { Name = "Adventure" }
      },
      Id = 1009,
      Name = "The Last of Us",
      ParentGame = null,
      Platforms = new List<Platform>() { new Platform() { Name = "PlayStation 3" } },
      TotalRating = 93
    };

    #endregion Private Members

    #region Public Methods

    [TestMethod]
    public async Task GetAllPlatforms()
    {
      List<Platform> platforms = await IGDBProvider.GetAllPlatforms();

      Assert.IsNotNull(platforms);
      Assert.IsTrue(platforms.Count > 0);
    }

    [TestMethod]
    public async Task GetGameInvalidName()
    {
      Game gameData = await IGDBProvider.GetGame("ThereIsNoGameWithThisName");

      Assert.IsNull(gameData);
    }

    [TestMethod]
    public async Task GetGameValidName()
    {
      Game gameData = await IGDBProvider.GetGame("The Last of Us");

      Assert.IsNotNull(gameData);

      Assert.AreEqual(_theLastOfUsData.Collection.Name, gameData.Collection.Name);
      Assert.AreEqual(_theLastOfUsData.Cover.Id, gameData.Cover.Id);
      Assert.AreEqual(_theLastOfUsData.Expansions.First().Name, gameData.Expansions.First().Name);
      Assert.AreEqual(_theLastOfUsData.FirstReleaseDate, gameData.FirstReleaseDate);
      Assert.AreEqual(_theLastOfUsData.Genres[0].Name, gameData.Genres[0].Name);
      Assert.AreEqual(_theLastOfUsData.Genres[1].Name, gameData.Genres[1].Name);
      Assert.AreEqual(_theLastOfUsData.Id, gameData.Id);
      Assert.AreEqual(_theLastOfUsData.Name, gameData.Name);
      Assert.AreEqual(_theLastOfUsData.Platforms.First().Name, gameData.Platforms.First().Name);
      Assert.IsTrue(gameData.TotalRating != 0);
    }

    [TestMethod]
    public async Task GetMultipleGamesInvalidNames()
    {
      List<string> gameNames = new List<string>()
      {
        "ThereIsNoGameWithThisName",
        "ThereIsNoGameWithThisName:TheSequel"
      };

      List<Game> gameData = await IGDBProvider.GetMultipleGames(gameNames);

      Assert.IsNotNull(gameData);
      Assert.IsNull(gameData[0]);
      Assert.IsNull(gameData[1]);
    }

    [TestMethod]
    public async Task GetMultipleGamesValidNames()
    {
      List<string> gameNames = new List<string>()
      {
        "The Last of Us",
        "The Last of Us Remastered"
      };

      List<Game> gameData = await IGDBProvider.GetMultipleGames(gameNames);

      // The Last of Us
      Assert.AreEqual(_theLastOfUsData.Collection.Name, gameData[0].Collection.Name);
      Assert.AreEqual(_theLastOfUsData.Cover.Id, gameData[0].Cover.Id);
      Assert.AreEqual(_theLastOfUsData.Expansions.First().Name,
        gameData[0].Expansions.First().Name);
      Assert.AreEqual(_theLastOfUsData.FirstReleaseDate, gameData[0].FirstReleaseDate);
      Assert.AreEqual(_theLastOfUsData.Genres[0].Name, gameData[0].Genres[0].Name);
      Assert.AreEqual(_theLastOfUsData.Genres[1].Name, gameData[0].Genres[1].Name);
      Assert.AreEqual(_theLastOfUsData.Id, gameData[0].Id);
      Assert.AreEqual(_theLastOfUsData.Name, gameData[0].Name);
      Assert.AreEqual(_theLastOfUsData.Platforms.First().Name, gameData[0].Platforms.First().Name);
      Assert.IsTrue(gameData[0].TotalRating != 0);

      // The Last of Us Remastered
      Assert.AreEqual(_remasteredData.Collection.Name, gameData[1].Collection.Name);
      Assert.AreEqual(_remasteredData.Cover.Id, gameData[1].Cover.Id);
      Assert.AreEqual(_remasteredData.Dlcs.First().Name, gameData[1].Dlcs.First().Name);
      Assert.AreEqual(_remasteredData.FirstReleaseDate, gameData[1].FirstReleaseDate);
      Assert.AreEqual(_remasteredData.Genres[0].Name, gameData[1].Genres[0].Name);
      Assert.AreEqual(_remasteredData.Genres[1].Name, gameData[1].Genres[1].Name);
      Assert.AreEqual(_remasteredData.Id, gameData[1].Id);
      Assert.AreEqual(_remasteredData.Name, gameData[1].Name);
      Assert.AreEqual(_remasteredData.Platforms.First().Name, gameData[1].Platforms.First().Name);
      Assert.IsTrue(gameData[1].TotalRating != 0);
    }

    [TestMethod]
    public async Task SearchGameValidName()
    {
      List<Game> gameData = await IGDBProvider.SearchGame("The Last of Us");

      Assert.IsNotNull(gameData);
      Assert.IsTrue(gameData.Count == 10);

      foreach (Game result in gameData)
      {
        Assert.IsTrue(result.Name.Contains("The Last of Us"));
      }
    }

    #endregion Public Methods
  }
}