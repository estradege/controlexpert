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
        /// Get [STSource] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<StSource>> GetStSourceAsync()
        {
            return Task.Run(() => GetStSource());
        }

        /// <summary>
        /// Get [STSource] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StSource> GetStSource()
        {
            return xef.Elements()
                .Elements("program")
                .Elements("STSource")
                .Select(stSource =>
                {
                    var identProgramElm = stSource.Parent.Element("identProgram");
                    var identProgram = new IdentProgram
                    {
                        Name = identProgramElm?.Attribute("name")?.Value,
                        Type = identProgramElm?.Attribute("type")?.Value,
                        Task = identProgramElm?.Attribute("task")?.Value
                    };

                    return new StSource
                    {
                        IdentProgram = identProgram,
                        Content = stSource.Value
                    };
                });
        }
    }
}
