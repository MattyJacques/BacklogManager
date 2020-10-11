using System.IO;

namespace Database.Tests.Helpers
{
  internal class FileManager
  {
    #region Public Methods

    /// <summary>
    /// Create a directory at the given path
    /// </summary>
    /// <param name="path">Path to create a directory at</param>
    /// <returns>If the operation was successful</returns>
    public static bool CreateDirectory(string path)
    {
      if (!Directory.Exists(path) && !File.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      return Directory.Exists(path);
    }

    /// <summary>
    /// Check if a file exists, if so delete the file
    /// </summary>
    /// <param name="path">Path to the file to delete</param>
    /// <returns>If the operation was successful</returns>
    public static bool DeleteFile(string path)
    {
      if (File.Exists(path))
      {
        File.Delete(path);
      }

      return File.Exists(path);
    }

    #endregion Public Methods
  }
}