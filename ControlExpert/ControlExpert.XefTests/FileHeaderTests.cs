using ControlExpert.Xef;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class FileHeaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetFileHeaderM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var fileHeader = await xef.GetFileHeaderAsync();

            Assert.AreEqual("Schneider Automation", fileHeader.Company);
            Assert.True(fileHeader.Product.Contains("Control Expert"));
            Assert.GreaterOrEqual(fileHeader.DateTime.Year, 2021);
            Assert.AreEqual("Fichier source projet", fileHeader.Content);
            Assert.GreaterOrEqual(fileHeader.DtdVersion, 41);
        }
    }
}