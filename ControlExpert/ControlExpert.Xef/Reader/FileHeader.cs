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
        /// Get [fileHeader] tag
        /// </summary>
        /// <returns></returns>
        public async Task<FileHeader> GetFileHeaderAsync()
        {
            return await Task.Run(() => GetFileHeader());
        }

        /// <summary>
        /// Get [fileHeader] tag
        /// </summary>
        /// <returns></returns>
        public FileHeader GetFileHeader()
        {
            var fileHeader = xef.Elements()
                    .Elements("fileHeader")
                    .SingleOrDefault();

            var datetimeAtr = fileHeader?.Attribute("dateTime")?.Value ?? string.Empty;
            var datetime = Convert.ToDateTime(datetimeAtr
                .Replace("date_and_time#", "")
                .Replace("dt#", "")
                .Replace('-', ' '));

            var dtdVersionAtr = fileHeader?.Attribute("DTDVersion")?.Value ?? string.Empty;
            var dtdVersion = Convert.ToInt32(dtdVersionAtr);

            return new FileHeader
            {
                Company = fileHeader?.Attribute("company")?.Value ?? string.Empty,
                Product = fileHeader?.Attribute("product")?.Value ?? string.Empty,
                DateTime = datetime,
                Content = fileHeader?.Attribute("content")?.Value ?? string.Empty,
                DtdVersion = dtdVersion
            };
        }
    }
}
