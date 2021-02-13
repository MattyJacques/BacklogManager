using IGDB.API;
using IGDB.Models;
using IGDB.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestEase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGDB
{
  [Header("Accept", "application/json")]
  public interface IGDBApi
  {
    #region Public Properties

    /// <summary>
    /// OAuth Client ID to use with queries
    /// </summary>
    [Header("client-id")]
    string ClientID { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Query the IGDB API using the endpoint given. https://api-docs.igdb.com/#endpoints
    /// </summary>
    /// <typeparam name="T">Model object to deserialize query data</typeparam>
    /// <param name="endpoint">Name of IGDB endpoint</param>
    /// <param name="query">String containing query</param>
    /// <returns></returns>
    [Post("/{endpoint}")]
    Task<T[]> QueryAsync<T>([Path] string endpoint, [Body] string query = null);

    #endregion Public Methods
  }

  public class Client
  {
    #region Public Members

    public static JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings()
    {
      Converters = new List<JsonConverter>() {
         // new IdentityConverter(),
          new UnixTimestampConverter()
        },
      ContractResolver = new DefaultContractResolver()
      {
        NamingStrategy = new SnakeCaseNamingStrategy()
      }
    };

    #endregion Public Members

    #region Private Members

    private readonly IGDBApi _api;
    private readonly TokenHandler _tokenHandler;

    #endregion Private Members

    #region Public Constructors

    /// <summary>
    /// Create a RestEase client for the IGDB api
    /// </summary>
    public Client(string id, string secret)
    {
      _tokenHandler = new TokenHandler(new OAuthClient(id, secret));

      var api = new RestClient("https://api.igdb.com/v4", async (request, cancellationToken) =>
      {
        var twitchToken = await _tokenHandler.AcquireTokenAsync();

        if (twitchToken?.AccessToken != null)
        {
          request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer", twitchToken.AccessToken);
        }
      })
      {
        JsonSerializerSettings = DefaultJsonSerializerSettings
      }.For<IGDBApi>();

      api.ClientID = id;
      _api = api;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Whether or not the exception is due to a invalid token
    /// </summary>
    /// <param name="ex"></param>
    /// <remarks>See: https://dev.twitch.tv/docs/authentication</remarks>
    /// <returns>Whether the token was valid</returns>
    public static bool IsInvalidTokenResponse(ApiException ex)
    {
      return ex.StatusCode == System.Net.HttpStatusCode.Unauthorized &&
        ex.Headers.WwwAuthenticate.ToString().Contains("invalid_token");
    }

    public async Task<T[]> QueryAsync<T>(string endpoint, string query = null)
    {
      try
      {
        return await _api.QueryAsync<T>(endpoint, query);
      }
      catch (ApiException apiEx)
      {
        // Acquire new token and retry request (once)
        if (IsInvalidTokenResponse(apiEx))
        {
          await _tokenHandler.RefreshTokenAsync();

          return await _api.QueryAsync<T>(endpoint, query);
        }

        throw apiEx;
      }
    }

    #endregion Public Methods

    #region Public Classes

    /// <summary>
    /// https://api-docs.igdb.com/#endpoints
    /// </summary>
    public static class Endpoints
    {
      #region Public Members

      public const string AgeRating = "age_ratings";
      public const string AgeRatingContentDescriptions = "age_rating_content_descriptions";
      public const string AlternativeNames = "alternative_names";
      public const string Artworks = "artworks";
      public const string CharacterMugShots = "character_mug_shots";
      public const string Characters = "characters";
      public const string Collections = "collections";
      public const string Companies = "companies";
      public const string CompanyWebsites = "company_websites";
      public const string Covers = "covers";
      public const string ExternalGames = "external_games";
      public const string Franchies = "franchises";
      public const string GameEngineLogos = "game_engine_logos";
      public const string GameEngines = "game_engines";
      public const string GameModes = "game_modes";
      public const string Games = "games";
      public const string GameVersionFeatures = "game_version_features";
      public const string GameVersionFeatureValues = "game_version_feature_values";
      public const string GameVersions = "game_versions";
      public const string GameVideos = "game_videos";
      public const string Genres = "genres";
      public const string InvolvedCompanies = "involved_companies";
      public const string Keywords = "keywords";
      public const string MultiplayerModes = "multiplayer_modes";
      public const string PlatformFamilies = "platform_families";
      public const string PlatformLogos = "platform_logos";
      public const string Platforms = "platforms";
      public const string PlatformVersionCompanies = "platform_version_companies";
      public const string PlatformVersionReleaseDates = "platform_version_release_dates";
      public const string PlatformVersions = "platform_versions";
      public const string PlatformWebsites = "platform_websites";
      public const string PlayerPerspectives = "player_perspectives";
      public const string ReleaseDates = "release_dates";
      public const string Screenshots = "screenshots";
      public const string Search = "search";
      public const string Themes = "themes";
      public const string Websites = "websites";

      #endregion Public Members
    }

    #endregion Public Classes
  }
}