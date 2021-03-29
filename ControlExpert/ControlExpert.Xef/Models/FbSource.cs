using System;
using System.Collections.Generic;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [FBSource] tag
    /// </summary>
    public class FbSource
    {
        public string NameOfFbType { get; set; }
        public Version Version { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public IEnumerable<Variable> InputParameters { get; set; }
        public IEnumerable<Variable> OutputParameters { get; set; }
        public IEnumerable<Variable> InOutParameters { get; set; }
        public IEnumerable<Variable> PublicLocalVariables { get; set; }
        public IEnumerable<Variable> PrivateLocalVariables { get; set; }
    }
}
