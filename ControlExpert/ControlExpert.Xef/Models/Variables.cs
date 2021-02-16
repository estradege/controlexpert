using System.Collections.Generic;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [variables] tag
    /// </summary>
    public class Variables
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string Comment { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }
}
