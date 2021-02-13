using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGDB.Models
{
  public class Token
  {
    #region Public Properties

    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public string[] Scope { get; set; }
    public DateTimeOffset TokenAcquiredAt { get; set; }
    public string TokenType { get; set; }

    #endregion Public Properties

    #region Public Methods

    public bool HasTokenExpired()
    {
      return (DateTimeOffset.UtcNow - TokenAcquiredAt).TotalSeconds > ExpiresIn;
    }

    #endregion Public Methods
  }
}