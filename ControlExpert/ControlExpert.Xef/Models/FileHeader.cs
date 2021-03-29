using System;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [FileHeader] tag
    /// </summary>
    public class FileHeader
    {
        public string Company { get; set; }
        public string Product { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        public int DtdVersion { get; set; }
    }
}
