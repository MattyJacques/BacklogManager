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
    /// API key to use with queries
    /// </summary>
    [Header("user-key")]
    string ApiKey { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Return the API usage stats
    /// </summary>
    /// <returns></returns>
    [Get("/api_status")]
    Task<ApiStatus[]> GetApiStatus();

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

  public static class Client
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

    #region Public Methods

    /// <summary>
    /// Create a RestEase client for the IGDB api
    /// </summary>
    /// <returns>Return IGDB RestEase client</returns>
    public static IGDBApi Create()
    {
      RestClient client = new RestClient("https://api-v3.igdb.com");
      client.JsonSerializerSettings = DefaultJsonSerializerSettings;

      var api = client.For<IGDBApi>();
      api.ApiKey = Environment.GetEnvironmentVariable("IGDB_API_KEY");
      return api;
    }

    #endregion Public Methods

    #region Public Classes

    /// <summary>
    /// https://api-docs.igdb.com/#endpoints
    /// </summary>
    public static class Endpoints
    {
      #region Public Members

      public const string AchievementIcons = "achievement_icons";
      public const string Achievements = "achievements";
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
      public const string Feeds = "feeds";
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
      public const string PageBackgrounds = "page_backgrounds";
      public const string PageLogos = "page_logos";
      public const string Pages = "pages";
      public const string PlatformLogos = "platform_logos";
      public const string Platforms = "platforms";
      public const string PlatformVersionCompanies = "platform_version_companies";
      public const string PlatformVersionReleaseDates = "platform_version_release_dates";
      public const string PlatformVersions = "platform_versions";
      public const string PlatformWebsites = "platform_websites";
      public const string PlayerPerspectives = "player_perspectives";
      public const string ProductFamilies = "product_families";
      public const string PulseGroups = "pulse_groups";
      public const string Pulses = "pulses";
      public const string PulseSources = "pulse_sources";
      public const string PulseUrls = "pulse_urls";
      public const string ReleaseDates = "release_dates";
      public const string Screenshots = "screenshots";
      public const string Search = "search";
      public const string Themes = "themes";
      public const string TimeToBeats = "time_to_beats";
      public const string Titles = "titles";
      public const string Websites = "websites";

      #endregion Public Members
    }

    #endregion Public Classes
  }
}