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

        [Test]
        public async Task GetVariablesM340_InitialValue()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            var savedparam1 = variables.First(v => v.Name == "SAVEDPARAM1");
            Assert.AreEqual("123.4", savedparam1.VariableInit);
        }

        [Test]
        public async Task GetVariablesM340_NoInitialValue()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            var bit2 = variables.First(v => v.Name == "BIT2");
            Assert.Null(bit2.VariableInit);
        }

        [Test]
        public async Task GetVariablesM340_InstanceElements()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            var obj1 = variables.First(v => v.Name == "OBJ1");
            Assert.NotNull(obj1.InstanceElements);
        }

        [Test]
        public async Task GetVariablesM340_InstanceElements_SimpleElement()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            var obj1 = variables.First(v => v.Name == "OBJ1");
            var pub2 = obj1.InstanceElements.First(elm => elm.Name == "Pub2");

            Assert.AreEqual("tod#00:00:00", pub2.Value);
            Assert.IsNull(pub2.Parent);
            Assert.IsNull(pub2.Children);
        }

        [Test]
        public async Task GetVariablesM340_InstanceElements_NestedElement()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            var obj1 = variables.First(v => v.Name == "OBJ1");
            
            var pub1 = obj1.InstanceElements.First(elm => elm.Name == "Pub1");
            Assert.IsNull(pub1.Value);
            Assert.IsNull(pub1.Parent);
            Assert.NotNull(pub1.Children);

            var var1 = pub1.Children.First(elm => elm.Name == "Var1");

            Assert.AreEqual("FALSE", var1.Value);
            Assert.AreEqual("Pub1", var1.Parent.Name);
            Assert.IsNull(var1.Children);
        }

        [Test]
        public async Task GetVariablesM340_NoInstanceElements()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            var ton1 = variables.First(v => v.Name == "TON_1");
            Assert.Null(ton1.InstanceElements);
        }

        [Test]
        public async Task GetVariablesM340_TopologicalAddress()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m340.zef");
            var variables = await xef.GetVariablesAsync();

            var ddt1 = variables.First(v => v.Name == "DDT1");
            var ddt2 = variables.First(v => v.Name == "DDT2");
            Assert.AreEqual("%MW100", ddt1.TopologicalAddress);
            Assert.AreEqual("%MW125", ddt2.TopologicalAddress);
        }

        [Test]
        public async Task GetVariablesProcess()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m580-safety.zef");
            var variables = await xef.GetVariablesProcessAsync();

            Assert.GreaterOrEqual(variables.Count(), 2);
        }

        [Test]
        public async Task GetVariablesSafe()
        {
            var xef = new XefReader();
            await xef.LoadZefAsync("Stu/m580-safety.zef");
            var variables = await xef.GetVariablesSafeAsync();

            Assert.GreaterOrEqual(variables.Count(), 2);
        }
    }
}