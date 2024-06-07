using System.Diagnostics;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [Variables] tag
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class Variable
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string TopologicalAddress { get; set; }
        public string Comment { get; set; }
        public string VariableInit { get; set; }
        public IDictionary<string, string>? Attributes { get; set; }
        public IEnumerable<InstanceElement> InstanceElements { get; set; }
    }
}
