using System.Collections.Generic;

namespace IGDB.Models
{
  public class Collection : IIdentifier
  {
    #region Public Properties

    public List<GameName> Games { get; set; }

    public long? Id { get; set; }

    public string Name { get; set; }

    #endregion Public Properties
  }
}