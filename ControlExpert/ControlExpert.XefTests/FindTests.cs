using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class FindTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task FindFbInsideFbdSource()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var program = await xef.FindFirstOrDefaultAsync("Obj2");

            Assert.IsNotNull(program);
            Assert.AreEqual("Progr2", program.Name);
        }

        [Test]
        public async Task FindFbInsideStSource()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var program = await xef.FindFirstOrDefaultAsync("Obj1");

            Assert.IsNotNull(program);
            Assert.AreEqual("Progr1", program.Name);
        }

        [Test]
        public async Task FindEdtWriteInsideFbdSource()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var program = await xef.FindFirstOrDefaultAsync("bit3");

            Assert.IsNotNull(program);
            Assert.AreEqual("Progr2", program.Name);
        }

        [Test]
        public async Task FindEdtReadInsideFbdSource()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var program = await xef.FindFirstOrDefaultAsync("bit2");

            Assert.IsNotNull(program);
            Assert.AreEqual("Progr2", program.Name);
        }

        [Test]
        public async Task FindEdtInsideStSource()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var program = await xef.FindFirstOrDefaultAsync("bit5");

            Assert.IsNotNull(program);
            Assert.AreEqual("ProgrFast1", program.Name);
        }
    }
}