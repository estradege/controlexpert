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
        /// Get [FMDesc] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FmDesc>> GetFmDescAsync()
        {
            return Task.Run(() => GetFmDesc());
        }

        /// <summary>
        /// Get [FMDesc] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FmDesc> GetFmDesc()
        {
            return xef.Elements()
                .Elements("logicConf")
                .Elements("resource")
                .Descendants()
                .Elements("FMDesc")
                .Select(fmDesc => new FmDesc
                {
                    Name = fmDesc.Attribute("name").Value,
                    FmId = fmDesc.Attribute("FMId").Value,
                    FmParentId = fmDesc.Parent.Attribute("FMId")?.Value,
                });
        }
    }
}
