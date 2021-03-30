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
        /// Get [Variables] tags
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Variable>> GetVariablesAsync()
        {
            return Task.Run(() => GetVariables());
        }

        /// <summary>
        /// Get [Variables] tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Variable> GetVariables()
        {
            var datablock = xef.Elements().Elements("dataBlock");
            return GetVariables(datablock);
        }

        private IEnumerable<Variable> GetVariables(IEnumerable<XElement> elements)
        {
            var variables = elements.Elements("variables");
            return variables.Select(variable =>
            {
                return new Variable
                {
                    Name = variable.Attribute("name")?.Value,
                    TypeName = variable.Attribute("typeName")?.Value,
                    Comment = variable.Element("comment")?.Value,
                    Attributes = GetAttributesOrDefault(variable)
                };
            });
        }
    }
}
