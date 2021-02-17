using ControlExpert.Xef.Models;
using System;
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
        /// Get [variables] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Variables>> GetVariablesAsync()
        {
            return Task.Run(() => GetVariables());
        }

        /// <summary>
        /// Get [variables] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Variables> GetVariables()
        {
            var variables = xef.Elements()
                .Elements("dataBlock")
                .Elements("variables")
                .Select(variable =>
                {
                    var attributesElm = variable.Elements("attribute");
                    var attributes = attributesElm?.ToDictionary(a => a.Attribute("name").Value, a => a.Attribute("value").Value);

                    return new Variables
                    {
                        Name = variable.Attribute("name")?.Value,
                        TypeName = variable.Attribute("typeName")?.Value,
                        Comment = variable.Element("comment")?.Value,
                        Attributes = attributes
                    };
                });

            return variables;
        }
    }
}
