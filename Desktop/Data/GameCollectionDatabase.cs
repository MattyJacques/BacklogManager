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
  public class GameCollectionDatabase : IDisposable
  {
    #region Private Members

    private readonly Mode _dbMode;

    private readonly string _path;

    private SQLiteConnection _connection;

    #endregion Private Members

    #region Public Constructors

    /// <summary>
    /// Setup the controller for the game collection database
    /// </summary>
    /// <param name="path">Data source of the database, can be file or :memory:</param>
    /// <param name="dbMode">Mode of the database, can be closed after query or left open</param>
    public GameCollectionDatabase(string path = "", Mode dbMode = Mode.CloseAfterQuery)
    {
      if (String.IsNullOrEmpty(path))
      {
        _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                             "BacklogManager\\GameCollection.db");
      }
      else
      {
        _path = path;
      }

      _dbMode = dbMode;

      SetupDatabaseFile();
      SetupDatabaseConnection();
      SetupTable();
    }

    #endregion Public Constructors

    #region Public Enums

    public enum Mode
    {
      CloseAfterQuery,
      LeaveOpen
    };

    #endregion Public Enums

    #region Public Methods

    /// <summary>
    /// Add a game item to the database
    /// </summary>
    public bool AddGame(GameDatabaseEntry entry)
    {
      if (Open())
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

        int rowsAffected = command.ExecuteNonQuery();

        if (_dbMode == Mode.CloseAfterQuery)
        {
          Close();
        }

        return rowsAffected > 0;
      }

      return false;
    }

    /// <summary>
    /// Close the connection to the database
    /// </summary>
    public void Close() => _connection.Close();

    /// <summary>
    /// Delete an existing game entry in the database
    /// </summary>
    public bool DeleteGame(string gameName)
    {
      if (Open())
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "DELETE FROM Games WHERE GameName = @GameName";

        command.Parameters.Add(new SQLiteParameter("@GameName", gameName));

        int rowsAffected = command.ExecuteNonQuery();

        if (_dbMode == Mode.CloseAfterQuery)
        {
          Close();
        }

        return rowsAffected > 0;
      }

      return false;
    }

    /// <summary>
    /// Dispose the connection to the database
    /// </summary>
    public void Dispose()
    {
      Close();
      _connection.Dispose();
    }

    /// <summary>
    /// Edit an existing game item in the database
    /// </summary>
    public bool EditGame(string nameToEdit, GameDatabaseEntry entry)
    {
      if (Open())
      {
        if (DeleteGame(nameToEdit))
        {
          return AddGame(entry);
        }

        if (_dbMode == Mode.CloseAfterQuery)
        {
          Close();
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

      if (Open())
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

        if (_dbMode == Mode.CloseAfterQuery)
        {
          Close();
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
      if (Open())
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT count(GameName) FROM Games WHERE " + platform + " = 'true' AND PlayedStatus = '" + status + "'";

        object queryResult = command.ExecuteScalar();

        if (_dbMode == Mode.CloseAfterQuery)
        {
          Close();
        }

        return Convert.ToInt32(queryResult);
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
      if (Open())
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
        object result = command.ExecuteScalar();

        return result != null && result.ToString() == tableName;
      }

      return false;
    }

    /// <summary>
    /// Execute the a non-query command
    /// </summary>
    /// <param name="query"></param>
    private void ExecuteNonQuery(string query)
    {
      if (Open())
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
      }
    }

    private Task<DbDataReader> ExecuteQuery(string query)
    {
      if (Open())
      {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = query;
        return command.ExecuteReaderAsync();
      }

      return null;
    }

    /// <summary>
    /// Open a connection to the database
    /// </summary>
    /// <returns>Returns if the database is open</returns>
    private bool Open()
    {
      if (_connection.State != System.Data.ConnectionState.Open)
      {
        _connection.Open();
      }

      return _connection.State == System.Data.ConnectionState.Open;
    }

    /// <summary>
    /// Create the connection to the database file
    /// </summary>
    private void SetupDatabaseConnection()
    {
      string connectionString = string.Format("Data Source={0}", _path);
      _connection = new SQLiteConnection(connectionString);
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
          try
          {
            Directory.CreateDirectory(Path.GetDirectoryName(_path));
          }
          catch { }
        }

        try
        {
          SQLiteConnection.CreateFile(_path);
        }
        catch { }
      }
    }

    /// <summary>
    /// Create the table within the database
    /// </summary>
    private void SetupTable()
    {
      if (Open() && !CheckTableExists("Games"))
      {
        ExecuteNonQuery("CREATE TABLE " + DatabaseResources.TableNames_Games + " (" +
                        DatabaseResources.Column_GameName + " text NOT NULL PRIMARY KEY," +
                        DatabaseResources.Column_AddedDate + " text NOT NULL," +
                        DatabaseResources.Column_PC + " text NOT NULL," +
                        DatabaseResources.Column_PS3 + " text NOT NULL," +
                        DatabaseResources.Column_PS4 + " text NOT NULL," +
                        DatabaseResources.Column_PSVita + " text NOT NULL," +
                        DatabaseResources.Column_OwnedStatus + " text, " +
                        DatabaseResources.Column_PlayedStatus + " text NOT NULL);");
        ExecuteNonQuery("CREATE TABLE " + DatabaseResources.TableName_Settings + " (" +
                        DatabaseResources.Column_Key + " text NOT NULL PRIMARY KEY," +
                        DatabaseResources.Column_Value + " text NOT NULL);");
      }
    }

    /// <summary>
    /// Check to see if the existing database has the current schema version
    /// </summary>
    /// <returns></returns>
    private bool ShouldUpgrade()
    {
      bool result = true;

      if (CheckTableExists(DatabaseResources.TableName_Settings))
      {
        if (Open())
        {
          SQLiteCommand command = _connection.CreateCommand();
          command.CommandText = "SELECT " + DatabaseResources.Column_Value +
                                " FROM " + DatabaseResources.TableName_Settings +
                                " WHERE " + DatabaseResources.Column_Key + " = '" +
                                DatabaseResources.Settings_SchemaVersion + "'";
        }
      }

      return result;
    }

    #endregion Private Methods
  }
}