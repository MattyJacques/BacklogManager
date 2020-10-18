using Database.Game;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Tests.Helpers
{
  internal class DatabaseHelper
  {
    #region Public Methods

    /// <summary>
    /// Create a Database with the V0 schema
    /// </summary>
    /// <param name="path">Path the database should be created at</param>
    /// <returns>Whether the database was created</returns>
    public static bool CreateV0Database(string path)
    {
      SqliteConnection connection = new SqliteConnection(new SqliteConnectionStringBuilder()
      {
        DataSource = path
      }.ConnectionString);

      connection.Open();

      // Games table
      SqliteCommand command = connection.CreateCommand();
      command.CommandText = "CREATE TABLE Games (GameName text NOT NULL PRIMARY KEY," +
                            "AddedDate text NOT NULL, PC text NOT NULL, PS3 text NOT NULL," +
                            "PS4 text NOT NULL, PSVita text NOT NULL, OwnedStatus text, " +
                            "PlayedStatus text NOT NULL);";
      command.ExecuteNonQuery();

      // Settings table
      command.CommandText = "CREATE TABLE Settings (Key text NOT NULL PRIMARY KEY, " +
                            "Value text NOT NULL);";
      command.ExecuteNonQuery();

      // Add some data
      command.CommandText = "INSERT OR REPLACE INTO Games (GameName, AddedDate, PC, PS3, PS4, " +
                            "PSVita, OwnedStatus, PlayedStatus) VALUES ('The Last of Us', '" +
                             DateTime.Today.ToString() + "', 'False', 'True', 'False', " +
                             "'False', 'True', 'Complete')";
      command.ExecuteNonQuery();

      connection.Close();

      return CheckTableExists(path, "Games") && CheckTableExists(path, "Settings");
    }

    /// <summary>
    /// Drop a table from the database
    /// </summary>
    /// <param name="path">Path to the database file</param>
    /// <param name="tableName">Name of the table to drop</param>
    /// <returns>Whether the given table exists or not</returns>
    public static bool DropTable(string path, string tableName)
    {
      SqliteConnection connection = new SqliteConnection(new SqliteConnectionStringBuilder()
      {
        DataSource = path
      }.ConnectionString);

      connection.Open();

      // Games table
      SqliteCommand command = connection.CreateCommand();
      command.CommandText = "DROP TABLE Settings";
      command.ExecuteNonQuery();

      connection.Close();

      return !CheckTableExists(path, "Settings");
    }

    /// <summary>
    /// Get the current schema version of the database
    /// </summary>
    /// <param name="path">Path to existing database</param>
    /// <returns>Schema version as int</returns>
    public static int GetCurrentSchema(string path)
    {
      SqliteConnection connection = new SqliteConnection(new SqliteConnectionStringBuilder()
      {
        DataSource = path
      }.ConnectionString);

      connection.Open();

      SqliteCommand command = connection.CreateCommand();
      command.CommandText = "SELECT Value FROM Settings WHERE Key = 'SchemaVersion'";

      int result = Convert.ToInt32(command.ExecuteScalar());
      connection.Close();

      return result;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Check if a table with the given name exists in the database
    /// </summary>
    /// <param name="path">Path to the database</param>
    /// <param name="tableName">Name of the table to check</param>
    /// <returns>If the table exists in the database</returns>
    private static bool CheckTableExists(string path, string tableName)
    {
      SqliteConnection connection = new SqliteConnection(new SqliteConnectionStringBuilder()
      {
        DataSource = path
      }.ConnectionString);

      connection.Open();

      SqliteCommand command = connection.CreateCommand();
      command.CommandText = "SELECT count(*) FROM sqlite_master WHERE type = 'table' " +
                            "AND name = '" + tableName + "'";

      return 0 < Convert.ToInt32(command.ExecuteScalar());
    }

    #endregion Private Methods
  }
}