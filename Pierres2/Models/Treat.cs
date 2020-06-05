using System.Collections.Generic;

namespace Pierres2.Models
{
  public class Treat
  {
    public Treat()
    {
      this.Flavors = new HashSet<TreatFlavor>();
    }
    public int TreatId { get; set; }
    public string Type { get; set; }
    public virtual ICollection<TreatFlavor> Flavors { get; set; }
  }
}