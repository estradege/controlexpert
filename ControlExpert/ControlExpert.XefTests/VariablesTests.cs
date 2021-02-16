using ControlExpert.Xef;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ControlExpert.XefReaderTests
{
    public class VariablesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetVariablesM340()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            Assert.GreaterOrEqual(variables.Count(), 2);
        }
    }
}