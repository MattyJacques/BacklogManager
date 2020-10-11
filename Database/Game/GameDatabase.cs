using Database.Game.Models;
using Database.Properties;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Policy;

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
      //_connection = new SqliteConnection(String.Format("Filename=./" + _path));
      //_connection = new SqliteConnection(String.Format("Data Source=" + _path));
      _connection = new SqliteConnection(new SqliteConnectionStringBuilder()
      {
        DataSource = _path
      }.ConnectionString);

      SQLitePCL.Batteries.Init();

      SetupTable();
    }

    #endregion Public Constructors

    #region Private Enums

    private enum ColIndex
    {
      GameName = 0,
      AddedDate,
      PC,
      PS3,
      PS4,
      PSVita,
      OwnedStatus,
      PlayedStatus
    }

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
      return ExecuteNonQuery("INSERT OR REPLACE INTO " + Resources.TableNames_Games + " (" +
                             Resources.Column_GameName + ", " +
                             Resources.Column_AddedDate + ", " +
                             Resources.Column_PC + ", " +
                             Resources.Column_PS3 + ", " +
                             Resources.Column_PS4 + ", " +
                             Resources.Column_PSVita + ", " +
                             Resources.Column_OwnedStatus + ", " +
                             Resources.Column_PlayedStatus +
                             ") VALUES ('" +
                             entry.GameName + "', '" +
                             entry.AddedDate + "', '" +
                             entry.PC + "', '" +
                             entry.PS3 + "', '" +
                             entry.PS4 + "', '" +
                             entry.PSVita + "', '" +
                             entry.OwnedStatus + "', '" +
                             entry.PlayedStatus + "')");
    }

    /// <summary>
    /// Delete a game from the database
    /// </summary>
    /// <param name="gameName">Name of the game to delete</param>
    /// <returns>Whether the operation was successful</returns>
    public bool DeleteGame(string gameName)
    {
      return ExecuteNonQuery("DELETE FROM " + Resources.TableNames_Games +
                             " WHERE " + Resources.Column_GameName + " = '" + gameName + "'");
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

      if (ExecuteQuery("SELECT * FROM " + Resources.TableNames_Games) is SqliteDataReader reader)
      {
        while (reader.Read())
        {
          gameList.Add(
            new GameDatabaseEntry
            {
              GameName = reader.GetString((int)ColIndex.GameName),
              AddedDate = reader.GetString((int)ColIndex.AddedDate),
              PC = reader.GetString((int)ColIndex.PC),
              PS3 = reader.GetString((int)ColIndex.PS3),
              PS4 = reader.GetString((int)ColIndex.PS4),
              PSVita = reader.GetString((int)ColIndex.PSVita),
              OwnedStatus = reader.GetString((int)ColIndex.OwnedStatus),
              PlayedStatus = reader.GetString((int)ColIndex.PlayedStatus)
            });
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
                            Resources.TableNames_Games + " WHERE " + platform + " = 'true' AND " +
                            Resources.Column_PlayedStatus + " = '" + status + "'");
    }

    #endregion Public Methods

    #region Private Methods

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
    /// If the tables do not exist, create them
    /// </summary>
    private void SetupTable()
    {
      if (!CheckTableExists("Games"))
      {
        ExecuteNonQuery("CREATE TABLE " + Resources.TableNames_Games + " (" +
                        Resources.Column_GameName + " text NOT NULL PRIMARY KEY," +
                        Resources.Column_AddedDate + " text NOT NULL," +
                        Resources.Column_PC + " text NOT NULL," +
                        Resources.Column_PS3 + " text NOT NULL," +
                        Resources.Column_PS4 + " text NOT NULL," +
                        Resources.Column_PSVita + " text NOT NULL," +
                        Resources.Column_OwnedStatus + " text, " +
                        Resources.Column_PlayedStatus + " text NOT NULL);");
        ExecuteNonQuery("CREATE TABLE " + Resources.TableName_Settings + " (" +
                        Resources.Column_Key + " text NOT NULL PRIMARY KEY," +
                        Resources.Column_Value + " text NOT NULL);");
      }
    }

    /// <summary>
    /// Check to see if the existing database has the current schema version
    /// </summary>
    /// <returns>If the </returns>
    private bool ShouldUpgrade()
    {
      bool result = true;

      if (CheckTableExists(Resources.TableName_Settings))
      {
        if (Open())
        {
          result = Settings.ExecuteScalar("SELECT " + Resources.Column_Value +
                        " FROM " + Resources.TableName_Settings +
                        " WHERE " + Resources.Column_Key + " = '" +
                        Resources.Settings_SchemaVersion + "'");
        }
      }

      return result;
    }

    #endregion Private Methods
  }
}