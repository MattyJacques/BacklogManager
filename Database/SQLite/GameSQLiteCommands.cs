using Database.Interfaces;
using Database.Properties;
using Database.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLite
{
  internal abstract class GameSQLiteCommands
  {
    #region Private Members

    private readonly SqliteConnection _connection;

    #endregion Private Members

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
    /// Check if the connection is open, if it isn't open it
    /// </summary>
    /// <returns>Whether the connection is/has been opened</returns>
    public abstract bool Open();

    #endregion Public Methods

    #region Protected Methods

    /// <summary>
    /// Add a game to the database
    /// </summary>
    /// <param name="entry">Game data to add to the database</param>
    /// <returns>Whether the operation was successful</returns>
    protected bool AddGame(GameDatabaseEntry entry)
    {
      return ExecuteNonQuery("INSERT OR REPLACE INTO " + Resources.TableNames_Games + " (" +
                             Resources.Column_GameName +
                             Resources.Column_AddedDate +
                             Resources.Column_PC +
                             Resources.Column_PS3 +
                             Resources.Column_PS4 +
                             Resources.Column_PSVita +
                             Resources.Column_OwnedStatus +
                             Resources.Column_PlayedStatus +
                             ") VALUES (" +
                             entry.GameName + ", " +
                             entry.AddedDate + ", " +
                             entry.PC + ", " +
                             entry.PS3 + ", " +
                             entry.PS4 + ", " +
                             entry.PSVita + ", " +
                             entry.OwnedStatus + ", " +
                             entry.PlayedStatus + ")");
    }

    /// <summary>
    /// Delete a game from the database
    /// </summary>
    /// <param name="gameName">Name of the game to delete</param>
    /// <returns>Whether the operation was successful</returns>
    protected bool DeleteGame(string gameName)
    {
      return ExecuteNonQuery("DELETE FROM " + Resources.TableNames_Games +
                             " WHERE " + Resources.Column_GameName + " = " + gameName);
    }

    /// <summary>
    /// Edit an existing entry in the database
    /// </summary>
    /// <returns>Whether the operation was successful</returns>
    protected bool EditGame(string nameToEdit, GameDatabaseEntry entry)
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
    protected List<GameDatabaseEntry> GetAllGames()
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

      return gameList;
    }

    /// <summary>
    /// Get the amount of entries that have the provided played status on the specified platform
    /// </summary>
    /// <returns></returns>
    protected int GetAmountWithPlatformStatus(string platform, string status)
    {
      return ExecuteScalar("SELECT count(" + Resources.Column_GameName + ") FROM " +
                            Resources.TableNames_Games + " WHERE " + platform + " = 'true' AND " +
                            Resources.Column_PlayedStatus + " = '" + status + "'");
    }

    #endregion Protected Methods

    #region Private Methods

    /// <summary>
    /// Execute a non-query command
    /// </summary>
    /// <param name="commandText">Text of the command to execute</param>
    /// <returns>Number of rows affected</returns>
    private bool ExecuteNonQuery(string commandText)
    {
      int rowsAffected = 0;

      if (Open())
      {
        SqliteCommand command = _connection.CreateCommand();
        command.CommandText = commandText;
        rowsAffected = command.ExecuteNonQuery();
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
    /// <returns>Int containing the result of the query</returns>
    private int ExecuteScalar(string query)
    {
      int result = 0;

      if (Open())
      {
        SqliteCommand command = _connection.CreateCommand();
        command.CommandText = query;
        result = Convert.ToInt32(command.ExecuteScalar());
      }

      return result;
    }

    #endregion Private Methods
  }
}