namespace IGDB.Models
{
  public enum PlatformCategory
  {
    Console = 1,
    Arcade = 2,
    Platform = 3,
    OperatingSystem = 4,
    PortableConsole = 5,
    Computer = 6
  }

  public class Platform : IIdentifier
  {
    #region Public Properties

    public string Abbreviation { get; set; }
    public string AlternativeName { get; set; }
    public long? Id { get; set; }
    public string Name { get; set; }
    public PlatformLogo PlatformLogo { get; set; }

    #endregion Public Properties
  }
}