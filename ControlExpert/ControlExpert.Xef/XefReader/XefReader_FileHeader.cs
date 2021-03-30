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
        /// Get [FileHeader] tag
        /// </summary>
        /// <returns></returns>
        public Task<FileHeader> GetFileHeaderAsync()
        {
            return Task.Run(() => GetFileHeader());
        }

        /// <summary>
        /// Get [FileHeader] tag
        /// </summary>
        /// <returns></returns>
        public FileHeader GetFileHeader()
        {
            var fileHeader = xef.Elements()
                    .Elements("fileHeader")
                    .Single();
                       
            var dtdVersionAtr = fileHeader.Attribute("DTDVersion")?.Value;
            var dtdVersion = Convert.ToInt32(dtdVersionAtr);

            return new FileHeader
            {
                Company = fileHeader.Attribute("company").Value,
                Product = fileHeader.Attribute("product").Value,
                DateTime = GetDatetimeOrDefault(fileHeader),
                Content = fileHeader.Attribute("content").Value,
                DtdVersion = dtdVersion
            };
        }
    }
}
