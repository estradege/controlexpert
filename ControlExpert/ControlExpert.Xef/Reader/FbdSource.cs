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
        /// Get [FBDSource] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FbdSource>> GetFbdSourceAsync()
        {
            return Task.Run(() => GetFbdSource());
        }

        /// <summary>
        /// Get [FBDSource] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FbdSource> GetFbdSource()
        {
            return xef.Elements()
                .Elements("program")
                .Elements("FBDSource")
                .Select(fdbSource =>
                {
                    var identProgramElm = fdbSource.Parent.Element("identProgram");
                    var identProgram = new IdentProgram
                    {
                        Name = identProgramElm?.Attribute("name")?.Value,
                        Type = identProgramElm?.Attribute("type")?.Value,
                        Task = identProgramElm?.Attribute("task")?.Value
                    };

                    var nbRowsAtr = fdbSource.Attribute("nbRows")?.Value;
                    var nbRows = Convert.ToInt32(nbRowsAtr);

                    var nbColumnsAtr = fdbSource.Attribute("nbColumns")?.Value;
                    var nbColumns = Convert.ToInt32(nbColumnsAtr);

                    return new FbdSource
                    {
                        IdentProgram = identProgram,
                        NbRows = nbRows,
                        NbColumns = nbColumns,
                        NetworkFbd = fdbSource.Element("networkFBD")
                    };
                });
        }
    }
}
