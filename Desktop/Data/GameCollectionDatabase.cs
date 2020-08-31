using Desktop.Data.Types;
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
    #region Private Members

    private readonly string _path;
    private SQLiteConnection _connection;

    #endregion Private Members

    #region Public Constructors

    public GameCollectionDatabase()
    {
      _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BacklogManager\\GameCollection.db");

      SetupDatabaseFile();
      SetupDatabaseConnection();
      SetupTable();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Add a game item to the database
    /// </summary>
    public bool AddGame(GameDatabaseEntry entry)
    {
      if (_connection.State == System.Data.ConnectionState.Open)
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "INSERT OR REPLACE INTO Games" +
                              "(GameName, AddedDate, PC, PS3, PS4, PSVita, OwnedStatus, PlayedStatus) " +
                              "VALUES " +
                              "(@GameName, @AddedDate, @PC, @PS3, @PS4, @PSVita, @OwnedStatus, @PlayedStatus)";

        command.Parameters.Add(new SQLiteParameter("@GameName", entry.GameName));
        command.Parameters.Add(new SQLiteParameter("@AddedDate", entry.AddedDate));
        command.Parameters.Add(new SQLiteParameter("@PC", entry.PC));
        command.Parameters.Add(new SQLiteParameter("@PS3", entry.PS3));
        command.Parameters.Add(new SQLiteParameter("@PS4", entry.PS4));
        command.Parameters.Add(new SQLiteParameter("@PSVita", entry.PSVita));
        command.Parameters.Add(new SQLiteParameter("@OwnedStatus", entry.OwnedStatus));
        command.Parameters.Add(new SQLiteParameter("@PlayedStatus", entry.PlayedStatus));

        return command.ExecuteNonQuery() > 0;
      }

      return false;
    }

    /// <summary>
    /// Delete an existing game entry in the database
    /// </summary>
    public bool DeleteGame(string gameName)
    {
      if (_connection.State == System.Data.ConnectionState.Open)
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "DELETE FROM Games WHERE GameName = @GameName";

        command.Parameters.Add(new SQLiteParameter("@GameName", gameName));

        return command.ExecuteNonQuery() > 0;
      }

      return false;
    }

    /// <summary>
    /// Edit an existing game item in the database
    /// </summary>
    public bool EditGame(string nameToEdit, GameDatabaseEntry entry)
    {
      if (_connection.State == System.Data.ConnectionState.Open)
      {
        if (DeleteGame(nameToEdit))
        {
          return AddGame(entry);
        }
      }

      return false;
    }

    /// <summary>
    /// Get all games from the game table
    /// </summary>
    public async Task<List<GameDatabaseEntry>> GetAllGames()
    {
      List<GameDatabaseEntry> gameList = new List<GameDatabaseEntry>();

      if (_connection.State == System.Data.ConnectionState.Open)
      {
        if (await ExecuteQuery("SELECT * FROM Games") is SQLiteDataReader reader)
        {
          while (reader.Read())
          {
            GameDatabaseEntry entry = new GameDatabaseEntry
            {
              GameName = reader.GetString((int)ColIndex.GameName),
              AddedDate = reader.GetString((int)ColIndex.AddedDate),
              PC = reader.GetString((int)ColIndex.PC),
              PS3 = reader.GetString((int)ColIndex.PS3),
              PS4 = reader.GetString((int)ColIndex.PS4),
              PSVita = reader.GetString((int)ColIndex.PSVita),
              OwnedStatus = reader.GetString((int)ColIndex.OwnedStatus),
              PlayedStatus = reader.GetString((int)ColIndex.PlayedStatus)
            };

            gameList.Add(entry);
          }
        }
      }

      return gameList;
    }

    /// <summary>
    /// Get the amount of games
    /// </summary>
    /// <returns></returns>
    public int GetAmountWithPlatformStatus(string platform, string status)
    {
      if (_connection.State == System.Data.ConnectionState.Open)
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT count(GameName) FROM Games WHERE " + platform + " = 'true' AND PlayedStatus = '" + status + "'";

        return Convert.ToInt32(command.ExecuteScalar());
      }

      return 0;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Check if a table with the given name exists in the database
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private bool CheckTableExists(string tableName)
    {
      SQLiteCommand command = _connection.CreateCommand();
      command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
      object result = command.ExecuteScalar();

      return result != null && result.ToString() == tableName;
    }

    /// <summary>
    /// Execute the a non-query command
    /// </summary>
    /// <param name="query"></param>
    private void ExecuteNonQuery(string query)
    {
      SQLiteCommand command = _connection.CreateCommand();
      command.CommandText = query;
      command.ExecuteNonQuery();
    }

    private Task<DbDataReader> ExecuteQuery(string query)
    {
      SQLiteCommand command = _connection.CreateCommand();
      command.CommandText = query;
      return command.ExecuteReaderAsync();
    }

    /// <summary>
    /// Create the connection to the database file
    /// </summary>
    private void SetupDatabaseConnection()
    {
      string connectionString = string.Format("Data Source={0}", _path);
      _connection = new SQLiteConnection(connectionString);
      _connection.OpenAsync();
    }

    /// <summary>
    /// Create the database file if it does not already exist
    /// </summary>
    private void SetupDatabaseFile()
    {
      if (!string.IsNullOrEmpty(_path) && !File.Exists(_path))
      {
        if (!Directory.Exists(Path.GetDirectoryName(_path)))
        {
          Directory.CreateDirectory(Path.GetDirectoryName(_path));
        }

        SQLiteConnection.CreateFile(_path);
      }
    }

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
    }

    #endregion Private Methods
  }
}