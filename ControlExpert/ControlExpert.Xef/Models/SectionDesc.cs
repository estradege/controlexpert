using System;

namespace ControlExpert.Xef.Models
{
    /// <summary>
    /// [SectionDesc] tag
    /// </summary>
    public class SectionDesc
    {
        public string Task { get; set; }
        public string Name { get; set; }
        public string FmName { get; set; }
        public string FmId { get; set; }
        public string FmOrder { get; set; }
        public int SectionOrder { get; set; }
    }
}
