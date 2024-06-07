using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class FmDescTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetFmDescM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var fmDesc = await xef.GetFmDescAsync();

            Assert.GreaterOrEqual(fmDesc.Count(), 3);

            var mf1_1 = fmDesc.First(s => s.Name == "MF1_1");
            Assert.AreEqual("18aK", mf1_1.FmId);
            Assert.AreEqual("186K", mf1_1.FmParentId);

            var mf1 = fmDesc.First(s => s.Name == "MF1");
            Assert.AreEqual("186K", mf1.FmId);
            Assert.AreEqual("ROOT", mf1.FmParentId);
        }
    }
}