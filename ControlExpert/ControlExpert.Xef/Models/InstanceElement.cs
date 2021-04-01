using System.Collections.Generic;
using System.Diagnostics;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [InstanceElementDesc] tag
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class InstanceElement
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public InstanceElement Parent { get; set; }
        public IEnumerable<InstanceElement> Children { get; set; }
    }
}
