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
        /// Get [FBSource] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FbSource>> GetFbSourceAsync()
        {
            return Task.Run(() => GetFbSource());
        }

        /// <summary>
        /// Get [FBSource] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FbSource> GetFbSource()
        {
            var fbsources = xef.Elements()
                .Elements("FBSource")
                .Select(fbsource =>
                {
                    return new FbSource
                    {
                        NameOfFbType = fbsource.Attribute("name")?.Value,
                        Version = VersionAttributeOrDefault(fbsource),
                        DateTime = DatetimeAttributeOrDefault(fbsource),
                        Comment = fbsource.Element("comment")?.Value,
                        Attributes = AttributesOrDefault(fbsource),
                        InputParameters = VariableElementsToVariables(fbsource.Elements("inputParameters")),
                        OutputParameters = VariableElementsToVariables(fbsource.Elements("outputParameters")),
                        InOutParameters = VariableElementsToVariables(fbsource.Elements("inOutParameters")),
                        PublicLocalVariables = VariableElementsToVariables(fbsource.Elements("publicLocalVariables")),
                        PrivateLocalVariables = VariableElementsToVariables(fbsource.Elements("privateLocalVariables")),
                    };
                });

            return fbsources;
        }
    }
}
