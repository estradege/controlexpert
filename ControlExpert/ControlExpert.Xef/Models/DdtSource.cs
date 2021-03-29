using System;
using System.Collections.Generic;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [DDTSource] tag
    /// </summary>
    public class DdtSource
    {
        public string DdtName { get; set; }
        public Version Version { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public IEnumerable<Variable> Structure { get; set; }
    }
}
