using ControlExpert.Xef.Models;
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
        /// Get [ContentHeader] tag
        /// </summary>
        /// <returns></returns>
        public Task<ContentHeader> GetContentHeaderAsync()
        {
            return Task.Run(() => GetContentHeader());
        }

        /// <summary>
        /// Get [ContentHeader] tag
        /// </summary>
        /// <returns></returns>
        public ContentHeader GetContentHeader()
        {
            var contentHeader = xef.Elements()
                .Elements("contentHeader")
                .Single();

            return new ContentHeader
            {
                Name = contentHeader.Attribute("name").Value,
                Version = GetVersionOrDefault(contentHeader),
                DateTime = GetDatetimeOrDefault(contentHeader)
            };
        }
    }
}
