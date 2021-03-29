using ControlExpert.Xef.Models;
using System;
using System.Collections.Generic;
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
        public Task LoadZefAsync(string path)
        {
            return Task.Run(() => LoadZef(path));
        }

        /// <summary>
        /// Load XEF Exchange file
        /// </summary>
        /// <param name="path">XEF file path</param>
        public Task LoadXefAsync(string path)
        {
            return Task.Run(() => LoadXef(path));
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

        /// <summary>
        /// Find the first occurance of a variable
        /// </summary>
        /// <param name="name">The variable's name</param>
        /// <returns></returns>
        /// <remarks>
        /// The search order is :
        /// 1- Search <paramref name="name"/> as a FB in a FBD source
        /// 2- Search <paramref name="name"/> as a FB in a ST source
        /// 3- Search <paramref name="name"/> as a variable (write) in a FBD source
        /// 4- Search <paramref name="name"/> as a variable (read) in a FBD source
        /// 5- Search <paramref name="name"/> as a variable in a ST source
        /// </remarks>
        public Task<IdentProgram> FindFirstOrDefaultAsync(string name)
        {
            return Task.Run(() => FindFirstOrDefault(name));
        }

        /// <summary>
        /// Find the first occurance of a variable
        /// </summary>
        /// <param name="name">The variable's name</param>
        /// <returns></returns>
        /// <remarks>
        /// The search order is :
        /// 1- Search <paramref name="name"/> as a FB in a FBD source
        /// 2- Search <paramref name="name"/> as a FB in a ST source
        /// 3- Search <paramref name="name"/> as a variable (write) in a FBD source
        /// 4- Search <paramref name="name"/> as a variable (read) in a FBD source
        /// 5- Search <paramref name="name"/> as a variable in a ST source
        /// </remarks>
        public IdentProgram FindFirstOrDefault(string name)
        {
            name = name.ToLower();
            IdentProgram program = null;

            #region #1

            var fbdSource = GetFbdSource();

            if (program == null)
            {
                program = fbdSource.FirstOrDefault(fbd =>
                {
                    return fbd.NetworkFbd
                        .Elements("FFBBlock")
                        .Attributes("instanceName")
                        .Any(instanceName => instanceName.Value.ToLower() == name);

                })?.IdentProgram;
            }

            #endregion
            #region #2

            var stSource = GetStSource();

            if (program == null)
            {
                program = stSource.FirstOrDefault(st =>
                {
                    return st.Content
                        .Replace(" ", "")
                        .ToLower()
                        .Contains($"{name}(");

                })?.IdentProgram;
            }

            #endregion
            #region #3

            if (program == null)
            {
                program = fbdSource.FirstOrDefault(fbd =>
                {
                    return fbd.NetworkFbd
                        .Elements("FFBBlock")
                        .Elements("descriptionFFB")
                        .Elements("outputVariable")
                        .Any(outputVariable => outputVariable.Attribute("effectiveParameter")?.Value.ToLower() == name);

                })?.IdentProgram;

            }

            #endregion
            #region #3

            if (program == null)
            {
                program = fbdSource.FirstOrDefault(fbd =>
                {
                    return fbd.NetworkFbd
                        .Elements("FFBBlock")
                        .Elements("descriptionFFB")
                        .Elements("inputVariable")
                        .Any(inputVariable => inputVariable.Attribute("effectiveParameter")?.Value.ToLower() == name);

                })?.IdentProgram;

            }

            #endregion
            #region #5

            if (program == null)
            {
                program = stSource.FirstOrDefault(st =>
                {
                    return st.Content
                        .Replace(" ", "")
                        .ToLower()
                        .Contains(name);

                })?.IdentProgram;
            }

            #endregion

            return program;
        }

        private DateTime DatetimeAttributeOrDefault(XElement element, string attribute = "dateTime")
        {
            var datetimeAtr = element.Attribute(attribute)?.Value;
            if (datetimeAtr == null)
            {
                return default(DateTime);
            }
            else
            {
                datetimeAtr = datetimeAtr
                    .Replace("date_and_time#", "")
                    .Replace("dt#", "")
                    .Replace('-', ' ');

                return Convert.ToDateTime(datetimeAtr);
            }
        }

        private Version VersionAttributeOrDefault(XElement element, string attribute = "version")
        {
            var versionAtr = element.Attribute(attribute)?.Value;
            if (versionAtr == null)
            {
                return default(Version);
            }
            else
            {
                return new Version(versionAtr);
            }
        }

        private Dictionary<string, string> AttributesOrDefault(XElement element)
        {
            var attributesElm = element.Elements("attribute");
            var attributes = attributesElm?.ToDictionary(a => a.Attribute("name").Value, a => a.Attribute("value").Value);

            return attributes;
        }
    }
}
