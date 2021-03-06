namespace IGDB.Models
{
  public class PlatformLogo : IIdentifier
  {
    public bool? AlphaChannel { get; set; }
    public bool? Animated { get; set; }
    public int? Height { get; set; }
    public long? Id { get; set; }
    public string ImageId { get; set; }
    public string Url { get; set; }
    public int? Width { get; set; }
  }
}