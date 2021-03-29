using ControlExpert.Xef.Models;
using System.Collections.Generic;
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
        /// Get [DDTSource] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DdtSource>> GetDdtSourceAsync()
        {
            return Task.Run(() => GetDdtSource());
        }

        /// <summary>
        /// Get [DDTSource] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DdtSource> GetDdtSource()
        {
            var ddtSources = xef.Elements()
                .Elements("DDTSource")
                .Select(ddt =>
                {
                    return new DdtSource
                    {
                        DdtName = ddt.Attribute("DDTName")?.Value,
                        Version = VersionAttributeOrDefault(ddt),
                        DateTime = DatetimeAttributeOrDefault(ddt),
                        Comment = ddt.Element("comment")?.Value,
                        Attributes = AttributesOrDefault(ddt),
                        Structure = VariableElementsToVariables(ddt.Elements("structure"))
                    };
                });

            return ddtSources;
        }
    }
}
