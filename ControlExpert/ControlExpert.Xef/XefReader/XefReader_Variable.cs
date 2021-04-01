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
            return elements
                .Elements("variables")
                .Select(variable =>
                {
                    var variableInit = variable.Element("variableInit");
                    return new Variable
                    {
                        Name = variable.Attribute("name")?.Value,
                        TypeName = variable.Attribute("typeName")?.Value,
                        Comment = variable.Element("comment")?.Value,
                        VariableInit = variableInit?.Attribute("value")?.Value,
                        Attributes = GetAttributesOrDefault(variable),
                        InstanceElements = GetInstanceElementsOrDefault(variable)
                    };
                });
        }

        private IEnumerable<InstanceElement> GetInstanceElementsOrDefault(XElement element, InstanceElement parent = null)
        {
            var instanceElements = element.Elements("instanceElementDesc");
            if (!instanceElements.Any())
            {
                return default;
            }
            else
            {
                var tree = new List<InstanceElement>();

                foreach (var instanceElement in instanceElements)
                {
                    var (node, leaf) = GetInstanceElement(instanceElement);
                    tree.Add(node);

                    BindChildToParent(parent, node);
                    BindParentToChild(parent, node);

                    if (!leaf)
                    {
                        node.Children = GetInstanceElementsOrDefault(instanceElement, node);
                    }
                }

                return tree;
            }
        }

        private (InstanceElement node, bool leaf) GetInstanceElement(XElement instanceElement)
        {
            var node = new InstanceElement
            {
                Name = instanceElement.Attribute("name")?.Value,
            };

            var valueElm = instanceElement.Element("value");
            if (valueElm == null)
            {
                return (node, false);
            }
            else
            {
                node.Value = valueElm.Value;
                return (node, true);
            }
        }

        private void BindChildToParent(InstanceElement parent, InstanceElement child)
        {
            child.Parent = parent;
        }

        private void BindParentToChild(InstanceElement parent, InstanceElement child)
        {
            if (parent != null)
            {
                if (parent.Children == null)
                {
                    parent.Children = new List<InstanceElement>();
                }

                parent.Children.Append(child);
            }
        }
    }
}
