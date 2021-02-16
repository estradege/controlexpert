using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class SectionDescTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetSectionDescM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var sectionDesc = await xef.GetSectionDescAsync();

            Assert.GreaterOrEqual(sectionDesc.Count(), 4);

            var progr1 = sectionDesc.First(s => s.Name == "Progr1");
            Assert.AreEqual("MAST", progr1.Task);
            Assert.AreEqual("MF1", progr1.FmName);
            Assert.AreEqual("f8K", progr1.FmId);
            Assert.AreEqual("0x0001", progr1.FmOrder);
            Assert.AreEqual(1, progr1.SectionOrder);
        }
    }
}