using System;
using System.IO;
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

    #endregion Private Members

    #region Public Methods

    [TestMethod]
    public void AddGameValidData()
    {
    }

    [TestMethod]
    public void DeleteGameValidData()
    {
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