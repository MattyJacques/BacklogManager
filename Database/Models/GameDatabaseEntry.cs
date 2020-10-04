using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
  public class GameDatabaseEntry
  {
    #region Public Properties

    public string AddedDate { get; set; }
    public string GameName { get; set; }
    public string OwnedStatus { get; set; }
    public string PC { get; set; }
    public string PlayedStatus { get; set; }
    public string PS3 { get; set; }
    public string PS4 { get; set; }
    public string PSVita { get; set; }

    #endregion Public Properties
  }
}