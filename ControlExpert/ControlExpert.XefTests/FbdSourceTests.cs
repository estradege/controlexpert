using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class FbdSourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetFbdSourceM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var fbdSource = await xef.GetFbdSourceAsync();

            Assert.GreaterOrEqual(fbdSource.Count(), 1);

            var fbdSource1 = fbdSource.First(s => s.IdentProgram.Name == "Progr2");
            Assert.AreEqual("section", fbdSource1.IdentProgram.Type);
            Assert.AreEqual("MAST", fbdSource1.IdentProgram.Task);
            Assert.AreEqual(24, fbdSource1.NbRows);
            Assert.AreEqual(36, fbdSource1.NbColumns);
            Assert.IsNotNull(fbdSource1.NetworkFbd);
        }
    }
}