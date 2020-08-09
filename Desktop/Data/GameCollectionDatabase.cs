using Desktop.Data.Types;
using Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Desktop.Data
{
  public class GameCollectionDatabase
  {
    #region Members

    SQLiteConnection connection;
    string path;

    #endregion

    #region Public Methods

    public GameCollectionDatabase()
    {
      path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BacklogManager\\GameCollection.db");

      SetupDatabaseFile();
      SetupDatabaseConnection();
      SetupTable();
    } // Constructor

    /// <summary>
    /// Get all games from the game table
    /// </summary>
    public async Task<List<GameDatabaseEntry>> GetAllGames()
    {
      SQLiteDataReader reader = await ExecuteQuery("SELECT * FROM Games") as SQLiteDataReader;
      List<GameDatabaseEntry> gameList = new List<GameDatabaseEntry>();

      if (reader != null)
      {
        while (reader.Read())
        {
          GameDatabaseEntry entry = new GameDatabaseEntry();
          entry.GameName = reader.GetString((int)ColIndex.GameName);
          entry.AddedDate = reader.GetString((int)ColIndex.AddedDate);
          entry.PC = reader.GetString((int)ColIndex.PC);
          entry.PS3 = reader.GetString((int)ColIndex.PS3);
          entry.PS4 = reader.GetString((int)ColIndex.PS4);
          entry.PSVita = reader.GetString((int)ColIndex.PSVita);
          entry.OwnedStatus = reader.GetString((int)ColIndex.OwnedStatus);
          entry.PlayedStatus = reader.GetString((int)ColIndex.PlayedStatus);

          gameList.Add(entry);
        }
      }

      return gameList;

    } // GetAllGames

    /// <summary>
    /// Add a game item to the database
    /// </summary>
    public void AddGame()
    {
      throw new NotImplementedException();
    } // AddGame

    /// <summary>
    /// Edit an existing game item in the database
    /// </summary>
    public void EditGame()
    {
      throw new NotImplementedException();
    } // EditGame

    /// <summary>
    /// Delete an existing game entry in the database
    /// </summary>
    public void DeleteGame()
    {
      throw new NotImplementedException();
    } // DeleteGame

    #endregion

    #region Implementation

    /// <summary>
    /// Create the database file if it does not already exist
    /// </summary>
    private void SetupDatabaseFile()
    {
      if (!string.IsNullOrEmpty(path) && !File.Exists(path))
      {
        string strang = Path.GetDirectoryName(path);
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
          Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        SQLiteConnection.CreateFile(path);
      }
    } // CreateDatabaseFile

    /// <summary>
    /// Create the connection to the database file
    /// </summary>
    private void SetupDatabaseConnection()
    {
      string connectionString = string.Format("Data Source={0}", path);
      connection = new SQLiteConnection(connectionString);
      connection.OpenAsync();
    } // CreateConnection

    /// <summary>
    /// Create the table within the database
    /// </summary>
    private void SetupTable()
    {
      if (!CheckTableExists("Games"))
      {
        ExecuteNonQuery("CREATE TABLE Games (" +
                        "GameName text NOT NULL PRIMARY KEY," +
                        "AddedDate text NOT NULL," +
                        "PC text NOT NULL," +
                        "PS3 text NOT NULL," +
                        "PS4 text NOT NULL," +
                        "PSVita text NOT NULL," +
                        "OwnedStatus text," +
                        "PlayedStatus text NOT NULL); ");
      }
    } // CreateTable

    /// <summary>
    /// Check if a table with the given name exists in the database
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private bool CheckTableExists(string tableName)
    {
      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
      var result = command.ExecuteScalar();

      return result != null && result.ToString() == tableName;
    } // CheckTableExists

    /// <summary>
    /// Execute the a non-query command
    /// </summary>
    /// <param name="query"></param>
    private void ExecuteNonQuery(string query)
    {
      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = query;
      command.ExecuteNonQuery();
    } // ExecuteQuery

    private Task<DbDataReader> ExecuteQuery(string query)
    {
      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = query;
      return command.ExecuteReaderAsync();
    } // ExecuteQuery

    #endregion // Implementation
  }
}
