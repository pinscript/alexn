using alexn.Extensions;
using NUnit.Framework;

namespace alexn.Test
{
    public class EnumerationExtensions
    {
        [Test]
        public void Can_Get_Description()
        {
            Assert.AreEqual("Ettan", Foo.One.GetDescription());
            Assert.AreEqual("Tvåan", Foo.Two.GetDescription());
            Assert.AreEqual("Three", Foo.Three.GetDescription());
        }

        public enum Foo
        {
            [System.ComponentModel.Description("Ettan")]
            One = 1,
            [System.ComponentModel.Description("Tvåan")]
            Two,
            Three
        }
    }
}