using System;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [ContentHeader] tag
    /// </summary>
    public class ContentHeader
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public DateTime DateTime { get; set; }
    }
}
