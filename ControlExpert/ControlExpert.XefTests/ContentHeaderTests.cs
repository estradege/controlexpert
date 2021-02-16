using ControlExpert.Xef;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class ContentHeaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetContentHeaderM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var contentHeader = await xef.GetContentHeaderAsync();

            Assert.AreEqual("Projet", contentHeader.Name);
            Assert.GreaterOrEqual(contentHeader.Version.Build, 1);
            Assert.GreaterOrEqual(contentHeader.DateTime.Year, 2021);
        }
    }
}