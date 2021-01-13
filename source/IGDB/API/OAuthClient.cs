using IGDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGDB.API
{
  public interface IOAuthAPI
  {
    #region Public Methods

    [Post("/oauth2/token")]
    Task<Token> GetOAuth2Token([Body(BodySerializationMethod.UrlEncoded)] IDictionary<string, string> data);

    #endregion Public Methods
  }

  public class OAuthClient
  {
    #region Private Members

    private readonly IOAuthAPI _api;
    private readonly string _id;
    private readonly string _secret;

    #endregion Private Members

    #region Public Constructors

    public OAuthClient(string id, string secret)
    {
      _id = id;
      _secret = secret;
      _api = new RestClient("https://id.twitch.tv")
      {
        JsonSerializerSettings = new JsonSerializerSettings()
        {
          ContractResolver = new DefaultContractResolver()
          {
            NamingStrategy = new SnakeCaseNamingStrategy()
          }
        }
      }.For<IOAuthAPI>();
    }

    #endregion Public Constructors

    #region Public Methods

    public Task<Token> GetClientCredentialTokenAsync()
    {
      return _api.GetOAuth2Token(new Dictionary<string, string>() {
        {"client_id", _id},
        {"client_secret", _secret},
        {"grant_type", "client_credentials"}
      });
    }

    #endregion Public Methods
  }
}