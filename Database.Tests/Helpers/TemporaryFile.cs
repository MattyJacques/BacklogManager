using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Database.Tests.Helpers
{
  internal class TemporaryFile : IDisposable
  {
    #region Public Constructors

    public TemporaryFile()
    {
      MethodBase method = new StackTrace().GetFrame(1).GetMethod();
      FilePath = Path.Combine(Path.GetTempPath(), "BacklogManager", method.Name);

      FileManager.CreateDirectory(Path.Combine(Path.GetTempPath(), "BacklogManager"));
    }

    #endregion Public Constructors

    #region Private Destructors

    ~TemporaryFile()
    {
      Dispose();
    }

    #endregion Private Destructors

    #region Public Properties

    /// <summary>
    /// Path to the temporary file
    /// </summary>
    public string FilePath { get; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Delete the file when disposing of this object
    /// </summary>
    public void Dispose()
    {
      FileManager.DeleteFile(FilePath);
    }

    #endregion Public Methods
  }
}