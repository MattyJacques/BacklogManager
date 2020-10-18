using Database.Game;
using Database.Game.Models;
using Database.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Database.Tests.Game
{
  [TestClass]
  public class GameDatabaseTests
  {
    #region Private Members

    private readonly GameDatabaseEntry _dbEntry = new GameDatabaseEntry()
    {
      GameName = "The Last of Us",
      AddedDate = DateTime.Today.ToString(),
      PC = false.ToString(),
      PS3 = true.ToString(),
      PS4 = false.ToString(),
      PSVita = false.ToString(),
      OwnedStatus = true.ToString(),
      PlayedStatus = Status.Complete.ToString()
    };

    #endregion Private Members

    #region Public Methods

    [TestMethod]
    public void AddGameValidData()
    {
      TemporaryFile file = new TemporaryFile();
      GameDatabase database = new GameDatabase(file.FilePath);

      bool didAddGame = database.AddGame(_dbEntry);
      Assert.IsTrue(didAddGame);

      List<GameDatabaseEntry> games = database.GetAllGames();
      Assert.IsTrue(games.Count == 1);

      GameDatabaseEntry gameEntry = games.First();

      Assert.AreEqual(gameEntry.GameName, _dbEntry.GameName);
      Assert.AreEqual(gameEntry.AddedDate, _dbEntry.AddedDate);
      Assert.AreEqual(gameEntry.PC, _dbEntry.PC);
      Assert.AreEqual(gameEntry.PS3, _dbEntry.PS3);
      Assert.AreEqual(gameEntry.PS4, _dbEntry.PS4);
      Assert.AreEqual(gameEntry.PSVita, _dbEntry.PSVita);
      Assert.AreEqual(gameEntry.OwnedStatus, _dbEntry.OwnedStatus);
      Assert.AreEqual(gameEntry.PlayedStatus, _dbEntry.PlayedStatus);
    }

    [TestMethod]
    public void DeleteGameValidData()
    {
      TemporaryFile file = new TemporaryFile();
      GameDatabase database = new GameDatabase(file.FilePath);

      bool didSetup = database.AddGame(_dbEntry);
      Assert.IsTrue(didSetup);

      bool didDelete = database.DeleteGame(_dbEntry.GameName);
      Assert.IsTrue(didDelete);

      List<GameDatabaseEntry> games = database.GetAllGames();
      Assert.IsTrue(games.Count == 0);
    }

    [TestMethod]
    public void EditGameValidData()
    {
      TemporaryFile file = new TemporaryFile();
      GameDatabase database = new GameDatabase(file.FilePath);

      bool didSetup = database.AddGame(_dbEntry);
      Assert.IsTrue(didSetup);

      GameDatabaseEntry editedEntry = new GameDatabaseEntry
      {
        GameName = "EditedGameName",
        AddedDate = DateTime.Today.AddDays(1).ToString(),
        PC = true.ToString(),
        PS3 = false.ToString(),
        PS4 = true.ToString(),
        PSVita = true.ToString(),
        OwnedStatus = false.ToString(),
        PlayedStatus = Status.NotPlayed.ToString()
      };

      bool didEdit = database.EditGame(_dbEntry.GameName, editedEntry);
      Assert.IsTrue(didEdit);

      List<GameDatabaseEntry> games = database.GetAllGames();
      Assert.IsTrue(games.Count == 1);

      GameDatabaseEntry gameEntry = games.First();

      Assert.AreEqual(gameEntry.GameName, editedEntry.GameName);
      Assert.AreEqual(gameEntry.AddedDate, editedEntry.AddedDate);
      Assert.AreEqual(gameEntry.PC, editedEntry.PC);
      Assert.AreEqual(gameEntry.PS3, editedEntry.PS3);
      Assert.AreEqual(gameEntry.PS4, editedEntry.PS4);
      Assert.AreEqual(gameEntry.PSVita, editedEntry.PSVita);
      Assert.AreEqual(gameEntry.OwnedStatus, editedEntry.OwnedStatus);
      Assert.AreEqual(gameEntry.PlayedStatus, editedEntry.PlayedStatus);
    }

    [TestMethod]
    public void UpgradesSchema()
    {
      TemporaryFile file = new TemporaryFile();

      bool didSetup = DatabaseHelper.CreateV0Database(file.FilePath);
      Assert.IsTrue(didSetup);

      didSetup = DatabaseHelper.DropTable(file.FilePath, "Settings");
      Assert.IsTrue(didSetup);

      GameDatabase database = new GameDatabase(file.FilePath);

      bool didUpgrade = DatabaseHelper.GetCurrentSchema(file.FilePath) ==
                        Convert.ToInt32(ConfigurationManager.AppSettings["SchemaVersion"]);
      Assert.IsTrue(didUpgrade);

      List<GameDatabaseEntry> games = database.GetAllGames();
      Assert.IsTrue(games.Count == 1);

      GameDatabaseEntry gameEntry = games.First();

      Assert.AreEqual(gameEntry.GameName, _dbEntry.GameName);
      Assert.AreEqual(gameEntry.AddedDate, _dbEntry.AddedDate);
      Assert.AreEqual(gameEntry.PC, _dbEntry.PC);
      Assert.AreEqual(gameEntry.PS3, _dbEntry.PS3);
      Assert.AreEqual(gameEntry.PS4, _dbEntry.PS4);
      Assert.AreEqual(gameEntry.PSVita, _dbEntry.PSVita);
      Assert.AreEqual(gameEntry.OwnedStatus, _dbEntry.OwnedStatus);
      Assert.AreEqual(gameEntry.PlayedStatus, _dbEntry.PlayedStatus);
    }

    #endregion Public Methods
  }
}