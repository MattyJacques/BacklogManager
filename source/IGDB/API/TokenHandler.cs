using IGDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGDB.API
{
  internal class TokenHandler
  {
    #region Private Members

    private readonly OAuthClient _client;

    #endregion Private Members

    #region Public Constructors

    public TokenHandler(OAuthClient client)
    {
      _client = client;
    }

    #endregion Public Constructors

    #region Private Properties

    private static Token CurrentToken { get; set; }

    #endregion Private Properties

    #region Public Methods

    public async Task<Token> AcquireTokenAsync()
    {
      var currentToken = await GetTokenAsync();
      if (currentToken?.HasTokenExpired() == false)
      {
        return currentToken;
      }

      return await RefreshTokenAsync();
    }

    public async Task<Token> RefreshTokenAsync()
    {
      var accessToken = await _client.GetClientCredentialTokenAsync();
      accessToken.TokenAcquiredAt = DateTimeOffset.UtcNow;
      var storedToken = StoreToken(accessToken);

      return storedToken;
    }

    #endregion Public Methods

    #region Private Methods

    private Task<Token> GetTokenAsync()
    {
      return Task.FromResult(CurrentToken);
    }

    private Token StoreToken(Token token)
    {
      CurrentToken = token;
      return token;
    }

    #endregion Private Methods
  }
}