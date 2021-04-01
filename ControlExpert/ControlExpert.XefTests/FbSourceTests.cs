using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class FbSourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetFbSourceM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var fbSources = await xef.GetFbSourceAsync();

            Assert.GreaterOrEqual(fbSources.Count(), 1);

            var dfbtype1 = fbSources.First(s => s.NameOfFbType == "DFBTYPE1");
            Assert.AreEqual(1, dfbtype1.InputParameters.Count());
            Assert.AreEqual(1, dfbtype1.OutputParameters.Count());
            Assert.AreEqual(1, dfbtype1.InOutParameters.Count());
            Assert.AreEqual(4, dfbtype1.PublicLocalVariables.Count());
            Assert.AreEqual(4, dfbtype1.PrivateLocalVariables.Count());
        }
    }
}