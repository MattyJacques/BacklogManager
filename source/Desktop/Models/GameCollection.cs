using Desktop.Extensions.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
  public class GameCollection
  {
    #region Private Members

    private List<string> _games = new List<string>();
    private string _name = "";

    #endregion Private Members

    #region Public Properties

    /// <summary>
    /// List of game names in the collection
    /// </summary>
    public List<string> Games { get => _games; set => _games = value; }

    /// <summary>
    /// Name of the collection
    /// </summary>
    public string Name { get => _name; set => _name = value; }

    #endregion Public Properties
  }
}