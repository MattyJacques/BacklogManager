using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Data;
using Desktop.Data.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Desktop.Tests
{
  [TestClass]
  public class GameCollectionDatabaseTests
  {
    #region Private Members

    private GameDatabaseEntry _dbEntry = new GameDatabaseEntry()
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

    private string _dbPath = Path.Combine(Path.GetTempPath(), "BacklogManagerTests.db");

    #endregion Private Members

    #region Public Methods

    [TestMethod]
    public async Task AddGameValidData()
    {
      GameCollectionDatabase database =
        new GameCollectionDatabase(":memory:", GameCollectionDatabase.Mode.LeaveOpen);

      bool didAddGame = database.AddGame(_dbEntry);
      Assert.IsTrue(didAddGame);

      List<GameDatabaseEntry> games = await database.GetAllGames();
      Assert.IsTrue(games.Count > 0);

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
    public async Task DeleteGameValidData()
    {
      GameCollectionDatabase database =
        new GameCollectionDatabase(":memory:", GameCollectionDatabase.Mode.LeaveOpen);

      bool didSetup = database.AddGame(_dbEntry);
      Assert.IsTrue(didSetup);

      bool didDelete = database.DeleteGame(_dbEntry.GameName);
      Assert.IsTrue(didDelete);

      List<GameDatabaseEntry> games = await database.GetAllGames();
      Assert.IsTrue(games.Count == 0);
    }

    [TestMethod]
    public void EditGameValidData()
    {
    }

    [TestMethod]
    public void UpgradesSchema()
    {
    }

    #endregion Public Methods
  }
}