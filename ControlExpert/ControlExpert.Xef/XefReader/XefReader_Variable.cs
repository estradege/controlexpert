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
            var variables = xef.Elements()
                .Elements("dataBlock")
                .Elements("variables");

            return VariableElementsToVariables(variables);
        }

        private IEnumerable<Variable> VariableElementsToVariables(IEnumerable<XElement> variables)
        {
            return variables.Select(variable =>
            {
                return new Variable
                {
                    Name = variable.Attribute("name")?.Value,
                    TypeName = variable.Attribute("typeName")?.Value,
                    Comment = variable.Element("comment")?.Value,
                    Attributes = AttributesOrDefault(variable)
                };
            });
        }
    }
}
