using System.Xml.Linq;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [FBDSource] tag
    /// </summary>
    public class FbdSource
    {
        public IdentProgram IdentProgram { get; set; }
        public int NbRows { get; set; }
        public int NbColumns { get; set; }
        public XElement NetworkFbd { get; set; }
    }
}
