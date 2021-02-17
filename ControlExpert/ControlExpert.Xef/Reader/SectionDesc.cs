using ControlExpert.Xef.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ControlExpert.Xef
{
    /// <summary>
    /// *.xef file reader
    /// </summary>
    public partial class XefReader
    {
        /// <summary>
        /// Get [sectionDesc] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SectionDesc>> GetSectionDescAsync()
        {
            return Task.Run(() => GetSectionDesc());
        }

        /// <summary>
        /// Get [sectionDesc] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SectionDesc> GetSectionDesc()
        {
            return xef.Elements()
                .Elements("logicConf")
                .Elements("resource")
                .Elements("taskDesc")
                .Elements("sectionDesc")
                .Select(sectionDesc =>
                {
                    var sectionOrderAtr = sectionDesc.Attribute("SectionOrder")?.Value;
                    var sectionOrder = Convert.ToInt32(sectionOrderAtr);

                    return new SectionDesc
                    {
                        Task = sectionDesc.Parent.Attribute("task")?.Value,
                        Name = sectionDesc.Attribute("name")?.Value,
                        FmName = sectionDesc.Attribute("FMName")?.Value,
                        FmId = sectionDesc.Attribute("FMId")?.Value,
                        FmOrder = sectionDesc.Attribute("FMOrder")?.Value,
                        SectionOrder = sectionOrder,
                    };
                });
        }
    }
}
