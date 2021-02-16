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
        private XDocument xef;

        /// <summary>
        /// Load ZEF Exchange file
        /// </summary>
        /// <param name="path">ZEF file path</param>
        public async Task LoadZefAsync(string path)
        {
            await Task.Run(() => LoadZef(path));
        }

        /// <summary>
        /// Load XEF Exchange file
        /// </summary>
        /// <param name="path">XEF file path</param>
        public async Task LoadXefAsync(string path)
        {
            await Task.Run(() => LoadXef(path));
        }

        /// <summary>
        /// Load ZEF Exchange file
        /// </summary>
        /// <param name="path">ZEF file path</param>
        public void LoadZef(string path)
        {
            using (var archive = ZipFile.OpenRead(path))
            {
                path = path.Replace(".zef", ".xef");

                var xefFile = archive.Entries.FirstOrDefault(file => file.FullName.ToLower().EndsWith(".xef"));
                xefFile.ExtractToFile(path, true);
            }

            LoadXef(path);
        }

        /// <summary>
        /// Load XEF Exchange file
        /// </summary>
        /// <param name="path">XEF file path</param>
        public void LoadXef(string path)
        {
            xef = XDocument.Load(path);
        }
    }
}
