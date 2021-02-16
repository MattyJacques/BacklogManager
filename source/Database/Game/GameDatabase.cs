using Database.Game.Models;
using Database.Properties;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Database.Game
{
  public class GameDatabase
  {
    #region Private Members

    private readonly SqliteConnection _connection;
    private readonly string _path;

    #endregion Private Members

    #region Public Constructors

    /// <summary>
    /// Create a SQLite database object that holds game entries
    /// </summary>
    /// <param name="path"></param>
    public GameDatabase(string path = null)
    {
      // Setup database path
      _path = path ?? GetDefaultPath();

      // Setup database connection
      _connection = new SqliteConnection(new SqliteConnectionStringBuilder()
      {
        DataSource = _path
      }.ConnectionString);

      SQLitePCL.Batteries.Init();

      SetupTables();
    }

    #endregion Public Constructors

    #region Private Enums

    private enum GamesColIndex
    {
      GameName = 0,
      AddedDate,
      PC,
      PS3,
      PS4,
      PSVita,
      OwnedStatus,
      PlayedStatus,
      DownloadedData
    }

    private enum SettingsColIndex
    {
      Key = 0,
      Value
    };

    #endregion Private Enums

    #region Public Methods

    /// <summary>
    /// Get the default path for the database
    /// </summary>
    /// <returns>The default path for the database</returns>
    public static string GetDefaultPath()
    {
      return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                             "BacklogManager\\GameCollection.db");
    }

    /// <summary>
    /// Add a game to the database
    /// </summary>
    /// <param name="entry">Game data to add to the database</param>
    /// <returns>Whether the operation was successful</returns>
    public bool AddGame(GameDatabaseEntry entry)
    {
      return CheckTableExists(Resources.TableName_Games) &&
             ExecuteNonQuery("INSERT OR REPLACE INTO " + Resources.TableName_Games + " (" +
                             Resources.Column_GameName + ", " +
                             Resources.Column_AddedDate + ", " +
                             Resources.Column_PC + ", " +
                             Resources.Column_PS3 + ", " +
                             Resources.Column_PS4 + ", " +
                             Resources.Column_PSVita + ", " +
                             Resources.Column_OwnedStatus + ", " +
                             Resources.Column_PlayedStatus + ", " +
                             Resources.Column_DownloadedData +
                             ") VALUES (\"" +
                             entry.GameName + "\", \"" +
                             entry.AddedDate + "\", \"" +
                             entry.PC + "\", \"" +
                             entry.PS3 + "\", \"" +
                             entry.PS4 + "\", \"" +
                             entry.PSVita + "\", \"" +
                             entry.OwnedStatus + "\", \"" +
                             entry.PlayedStatus + "\", \"" +
                             entry.DownloadedData + "\")");
    }

    /// <summary>
    /// Add multiple games to the database
    /// </summary>
    /// <param name="games">List of all games to be added</param>
    /// <returns>Whether all games were added successfully</returns>
    public bool AddMultipleGames(List<GameDatabaseEntry> games)
    {
      bool result = true;

      foreach (GameDatabaseEntry entry in games)
      {
        // Keep trying to add games in case there was a one off error
        result = AddGame(entry) && result;
      }

      return result;
    }

    /// <summary>
    /// Add multiple settings to the database
    /// </summary>
    /// <param name="settings">List of all settings to be added</param>
    /// <returns>Whether all settings were added successfully</returns>
    public bool AddMultipleSettings(Dictionary<string, string> settings)
    {
      bool result = true;

      foreach (KeyValuePair<string, string> entry in settings)
      {
        // Keep trying to add games in case there was a one off error
        result = AddSetting(entry.Key, entry.Value) && result;
      }

      return result;
    }

    /// <summary>
    /// Delete a game from the database
    /// </summary>
    /// <param name="gameName">Name of the game to delete</param>
    /// <returns>Whether the operation was successful</returns>
    public bool DeleteGame(string gameName)
    {
      return ExecuteNonQuery("DELETE FROM " + Resources.TableName_Games +
                             " WHERE " + Resources.Column_GameName + " = \"" + gameName + "\"");
    }

    /// <summary>
    /// Edit an existing entry in the database
    /// </summary>
    /// <param name="nameToEdit">Old name of the entry to edit, in case it has changed</param>
    /// <param name="entry">New data to update the entry with</param>
    /// <returns>Whether the operation was successful</returns>
    public bool EditGame(string nameToEdit, GameDatabaseEntry entry)
    {
      if (DeleteGame(nameToEdit))
      {
        return AddGame(entry);
      }

      return false;
    }

    /// <summary>
    /// Get all the game entries from the database
    /// </summary>
    /// <returns>Return list containing all entries from the database</returns>
    public List<GameDatabaseEntry> GetAllGames()
    {
      List<GameDatabaseEntry> gameList = new List<GameDatabaseEntry>();

      if (CheckTableExists(Resources.TableName_Games) &&
          ExecuteQuery("SELECT * FROM " + Resources.TableName_Games) is SqliteDataReader reader)
      {
        while (reader.Read())
        {
          GameDatabaseEntry newGame = new GameDatabaseEntry();

          try
          {
            newGame.GameName = reader.GetString((int)GamesColIndex.GameName);
            newGame.AddedDate = reader.GetString((int)GamesColIndex.AddedDate);
            newGame.PC = reader.GetString((int)GamesColIndex.PC);
            newGame.PS3 = reader.GetString((int)GamesColIndex.PS3);
            newGame.PS4 = reader.GetString((int)GamesColIndex.PS4);
            newGame.PSVita = reader.GetString((int)GamesColIndex.PSVita);
            newGame.OwnedStatus = reader.GetString((int)GamesColIndex.OwnedStatus);
            newGame.PlayedStatus = reader.GetString((int)GamesColIndex.PlayedStatus);
            newGame.DownloadedData = reader.GetString((int)GamesColIndex.DownloadedData);
          }
          catch (Exception)
          {
            // Value was missing, hopefully it should not be vital
          }

          gameList.Add(newGame);
        }
      }

      Close();

      return gameList;
    }

    /// <summary>
    ///Get the amount of entries that have the provided played status on the specified platform
    /// </summary>
    /// <param name="platform">Platform to match with</param>
    /// <param name="status">Played status to match with</param>
    /// <returns>The amount of entries matching the played status and platform</returns>
    public int GetAmountWithPlatformStatus(string platform, string status)
    {
      return ExecuteScalar("SELECT count(" + Resources.Column_GameName + ") FROM " +
                            Resources.TableName_Games + " WHERE " + platform + " = 'true' AND " +
                            Resources.Column_PlayedStatus + " = '" + status + "'");
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Add a entry to the Settings table in the database
    /// </summary>
    /// <param name="key">Key name of the setting</param>
    /// <param name="value">Value of the setting</param>
    /// <returns></returns>
    private bool AddSetting(string key, string value)
    {
      return ExecuteNonQuery("INSERT OR REPLACE INTO " + Resources.TableName_Settings + " (" +
                             Resources.Column_Key + ", " + Resources.Column_Value +
                             ") VALUES ('" + key + "', '" + value + "')");
    }

    /// <summary>
    /// Check if a table with the given name exists in the database
    /// </summary>
    /// <param name="tableName">Name of the table to check</param>
    /// <returns>If the table exists in the database</returns>
    private bool CheckTableExists(string tableName)
    {
      return 0 < ExecuteScalar("SELECT count(*) FROM sqlite_master WHERE type = 'table' " +
                               "AND name = '" + tableName + "'");
    }

    private bool Close()
    {
      if (IsOpen())
      {
        _connection.Close();
      }

      return !IsOpen();
    }

    /// <summary>
    /// Execute a non-query command
    /// </summary>
    /// <param name="commandText">Text of the command to execute</param>
    /// <param name="shouldClose">Should the connection be closed afterwards</param>
    /// <returns>Number of rows affected</returns>
    private bool ExecuteNonQuery(string commandText, bool shouldClose = true)
    {
      int rowsAffected = 0;

      if (Open())
      {
        SqliteCommand command = _connection.CreateCommand();
        command.CommandText = commandText;
        rowsAffected = command.ExecuteNonQuery();

        if (shouldClose)
        {
          Close();
        }
      }

      return rowsAffected > 0;
    }

    /// <summary>
    /// Execute a query command
    /// </summary>
    /// <param name="query">Text of the query to execute</param>
    /// <returns>SqliteDataReader containing the query results</returns>
    private SqliteDataReader ExecuteQuery(string query)
    {
      SqliteDataReader reader = null;

      if (Open())
      {
        SqliteCommand command = _connection.CreateCommand();
        command.CommandText = query;
        reader = command.ExecuteReader();
      }

      return reader;
    }

    /// <summary>
    /// Execute a scalar query, such as to count entries with certain data
    /// </summary>
    /// <param name="query">Text of the query to execute</param>
    /// /// <param name="shouldClose">Should the connection be closed afterwards</param>
    /// <returns>Int containing the result of the query</returns>
    private int ExecuteScalar(string query, bool shouldClose = true)
    {
      int result = 0;

      if (Open())
      {
        SqliteCommand command = _connection.CreateCommand();
        command.CommandText = query;
        result = Convert.ToInt32(command.ExecuteScalar());

        if (shouldClose)
        {
          Close();
        }
      }

      return result;
    }

    /// <summary>
    /// Get a dictionary filled with all the settings from the settings table
    /// </summary>
    /// <returns>A dictionary filled with all the settings from the settings table</returns>
    private Dictionary<string, string> GetAllSettings()
    {
      Dictionary<string, string> settingList = new Dictionary<string, string>();

      if (CheckTableExists(Resources.TableName_Settings) &&
          ExecuteQuery("SELECT * FROM " + Resources.TableName_Settings) is SqliteDataReader reader)
      {
        while (reader.Read())
        {
          settingList.Add(
            reader.GetString((int)SettingsColIndex.Key),
            reader.GetString((int)SettingsColIndex.Value));
        }
      }

      return settingList;
    }

    /// <summary>
    /// Return whether the database connection is open or not
    /// </summary>
    /// <returns>True if database connection is open</returns>
    private bool IsOpen()
    {
      return _connection.State == System.Data.ConnectionState.Open;
    }

    /// <summary>
    /// Check if the connection is open, if it isn't open it
    /// </summary>
    /// <returns>Whether the connection is/has been opened</returns>
    private bool Open()
    {
      if (!IsOpen())
      {
        _connection.Open();
      }

      return IsOpen();
    }

    /// <summary>
    /// Rename the table with the given name to the required new name
    /// </summary>
    /// <param name="currentName">The current table name</param>
    /// <param name="newName">The new name for the table</param>
    /// <returns></returns>
    private bool RenameTable(string currentName, string newName)
    {
      ExecuteNonQuery("ALTER TABLE " + currentName + " RENAME TO " + newName);
      return !CheckTableExists(currentName) && CheckTableExists(newName);
    }

    /// <summary>
    /// Setup the table that will hold game entries
    /// </summary>
    /// <returns>Whether the table was created successfully</returns>
    private bool SetupGamesTable()
    {
      if (!CheckTableExists(Resources.TableName_Games))
      {
        ExecuteNonQuery("CREATE TABLE " + Resources.TableName_Games + " (" +
                        Resources.Column_GameName + " text NOT NULL PRIMARY KEY," +
                        Resources.Column_AddedDate + " text NOT NULL," +
                        Resources.Column_PC + " text NOT NULL," +
                        Resources.Column_PS3 + " text NOT NULL," +
                        Resources.Column_PS4 + " text NOT NULL," +
                        Resources.Column_PSVita + " text NOT NULL," +
                        Resources.Column_OwnedStatus + " text, " +
                        Resources.Column_PlayedStatus + " text NOT NULL," +
                        Resources.Column_DownloadedData + " text NOT NULL);");
      }

      return CheckTableExists(Resources.TableName_Games);
    }

    /// <summary>
    /// Setup the table that will hold database specific settings
    /// </summary>
    /// <returns>Whether the table was created successfully</returns>
    private bool SetupSettingsTable()
    {
      if (!CheckTableExists(Resources.TableName_Settings))
      {
        ExecuteNonQuery("CREATE TABLE " + Resources.TableName_Settings + " (" +
                        Resources.Column_Key + " text NOT NULL PRIMARY KEY," +
                        Resources.Column_Value + " text NOT NULL);");
      }

      return CheckTableExists(Resources.TableName_Settings);
    }

    /// <summary>
    /// If the tables do not exist, create them or upgrade them if needed
    /// </summary>
    private void SetupTables()
    {
      if (!Directory.Exists(Path.GetDirectoryName(_path)))
      {
        Directory.CreateDirectory(Path.GetDirectoryName(_path));
      }

      if (!File.Exists(_path))
      {
        SetupGamesTable();
        SetupSettingsTable();
      }
      else if (ShouldUpgrade())
      {
        UpgradeSchema();
      }
    }

    /// <summary>
    /// Check to see if the existing database has the current schema version
    /// </summary>
    /// <returns>True if the database schema does not have the same version</returns>
    private bool ShouldUpgrade()
    {
      bool result = false;

      if (CheckTableExists(Resources.TableName_Settings))
      {
        if (Open())
        {
          result = Settings.Default.SchemaVersion >
            ExecuteScalar("SELECT " + Resources.Column_Value +
                          " FROM " + Resources.TableName_Settings +
                          " WHERE " + Resources.Column_Key + " = '" +
                          Resources.Settings_SchemaVersion + "'");
        }
      }
      else
      {
        // If the settings table does not exist, upgrading will attempt to repair
        result = true;
      }

      return result;
    }

    /// <summary>
    /// Upgrade the Games table to fit the current schema
    /// </summary>
    /// <returns>Whether the table was upgraded</returns>
    private bool UpgradeGamesTable()
    {
      bool result = false;

      // Get the game data before we start messing with the tables
      List<GameDatabaseEntry> games = GetAllGames();

      if (!CheckTableExists(Resources.TableName_Games) ||
          RenameTable(Resources.TableName_Games,
                      Resources.TableName_GamesBackup + DateTime.Now.ToString("yyyyMMddTHHmmss")))
      {
        if (SetupGamesTable())
        {
          result = AddMultipleGames(games);
        }
      }

      return result;
    }

    /// <summary>
    /// Upgrade the database schema to the current version
    /// </summary>
    private void UpgradeSchema()
    {
      UpgradeGamesTable();
      UpgradeSettingsTable();
      AddSetting(Resources.Settings_SchemaVersion, Settings.Default.SchemaVersion.ToString());
    }

    /// <summary>
    /// Upgrade the Settings table to fit the current schema
    /// </summary>
    /// <returns>Whether the table was upgraded</returns>
    private bool UpgradeSettingsTable()
    {
      bool result = false;
      Dictionary<string, string> settings = GetAllSettings();

      if (!CheckTableExists(Resources.TableName_Settings) ||
          RenameTable(Resources.TableName_Settings,
                      Resources.TableName_SettingsBackup + DateTime.Now.ToString("yyyyMMddTHHmmss")))
      {
        if (SetupSettingsTable())
        {
          result = AddMultipleSettings(settings);
        }
      }

      return result;
    }

    #endregion Private Methods
  }
}