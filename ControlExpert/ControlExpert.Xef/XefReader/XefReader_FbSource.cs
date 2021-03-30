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
                        NameOfFbType = fbsource.Attribute("nameOfFBType")?.Value,
                        Version = GetVersionOrDefault(fbsource),
                        DateTime = GetDatetimeOrDefault(fbsource),
                        Comment = fbsource.Element("comment")?.Value,
                        Attributes = GetAttributesOrDefault(fbsource),
                        InputParameters = GetVariables(fbsource.Elements("inputParameters")),
                        OutputParameters = GetVariables(fbsource.Elements("outputParameters")),
                        InOutParameters = GetVariables(fbsource.Elements("inOutParameters")),
                        PublicLocalVariables = GetVariables(fbsource.Elements("publicLocalVariables")),
                        PrivateLocalVariables = GetVariables(fbsource.Elements("privateLocalVariables")),
                    };
                });

            return fbsources;
        }
    }
}
