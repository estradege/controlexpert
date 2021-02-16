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
                    .Single();

            var datetimeAtr = fileHeader.Attribute("dateTime")?.Value;
            var datetime = Convert.ToDateTime(datetimeAtr
                .Replace("date_and_time#", "")
                .Replace("dt#", "")
                .Replace('-', ' '));

            var dtdVersionAtr = fileHeader.Attribute("DTDVersion")?.Value;
            var dtdVersion = Convert.ToInt32(dtdVersionAtr);

            return new FileHeader
            {
                Company = fileHeader.Attribute("company").Value,
                Product = fileHeader.Attribute("product").Value,
                DateTime = datetime,
                Content = fileHeader.Attribute("content").Value,
                DtdVersion = dtdVersion
            };
        }
    }
}
