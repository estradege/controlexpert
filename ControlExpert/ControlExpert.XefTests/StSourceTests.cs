using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class StSourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetStSourceM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var stSource = await xef.GetStSourceAsync();

            Assert.GreaterOrEqual(stSource.Count(), 1);

            var stSource1 = stSource.First(s => s.IdentProgram.Name == "Progr1");
            Assert.AreEqual("section", stSource1.IdentProgram.Type);
            Assert.AreEqual("MAST", stSource1.IdentProgram.Task);
            Assert.IsNotNull(stSource1.Content);
        }
    }
}