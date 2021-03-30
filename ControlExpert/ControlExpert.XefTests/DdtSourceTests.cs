using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class DdtSourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetDdtSourceM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var ddtSources = await xef.GetDdtSourceAsync();

            Assert.GreaterOrEqual(ddtSources.Count(), 1);

            var ddttype1 = ddtSources.First(s => s.DdtName == "DDTTYPE1");
            Assert.AreEqual(3, ddttype1.Structure.Count());
        }
    }
}