using System;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [contentHeader] tag
    /// </summary>
    public class ContentHeader
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public DateTime DateTime { get; set; }
    }
}
