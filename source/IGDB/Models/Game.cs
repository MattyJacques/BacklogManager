using System;
using System.Collections.Generic;

namespace IGDB.Models
{
  public enum Category
  {
    MainGame = 0,
    DlcAddon = 1,
    Expansion = 2,
    Bundle = 3,
    StandaloneExpansion = 4
  }

  public enum GameStatus
  {
    Released = 0,
    Alpha = 2,
    Beta = 3,
    EarlyAccess = 4,
    Offline = 5,
    Cancelled = 6
  }

  public class Game : IIdentifier
  {
    #region Public Properties

    public Category? Category { get; set; }

    public Collection Collection { get; set; }

    public Cover Cover { get; set; }

    public List<GameName> Dlcs { get; set; }

    public List<GameName> Expansions { get; set; }

    public DateTimeOffset? FirstReleaseDate { get; set; }

    public List<Genre> Genres { get; set; }

    public long? Id { get; set; }

    public string Name { get; set; }

    public GameName ParentGame { get; set; }

    public List<Platform> Platforms { get; set; }

    public double? TotalRating { get; set; }

    #endregion Public Properties
  }
}