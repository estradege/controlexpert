using ControlExpert.Xef.Models;
using System;
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
        /// Get [contentHeader] tag
        /// </summary>
        /// <returns></returns>
        public async Task<ContentHeader> GetContentHeaderAsync()
        {
            return await Task.Run(() => GetContentHeader());
        }

        /// <summary>
        /// Get [contentHeader] tag
        /// </summary>
        /// <returns></returns>
        public ContentHeader GetContentHeader()
        {
            var contentHeader = xef.Elements()
                    .Elements("contentHeader")
                    .SingleOrDefault();

            var datetimeAtr = contentHeader?.Attribute("dateTime")?.Value ?? string.Empty;
            var datetime = Convert.ToDateTime(datetimeAtr
                .Replace("date_and_time#", "")
                .Replace("dt#", "")
                .Replace('-', ' '));

            var versionAtr = contentHeader?.Attribute("version")?.Value ?? string.Empty;
            var version = new Version(versionAtr);

            return new ContentHeader
            {
                Name = contentHeader?.Attribute("name")?.Value ?? string.Empty,
                Version = version,
                DateTime = datetime
            };
        }
    }
}
