using alexn.Extensions;
using NUnit.Framework;

namespace alexn.Test {
    [TestFixture]
    public class NullableExtensionsTest {
        [Test]
        public void Can_Get_Default_When_Null() {
            int? original = null;
            Assert.AreEqual(1, original.ValueOr(1));
        }

        [Test]
        public void Can_Get_Value_When_Not_Null() {
            int? original = 1;
            Assert.AreEqual(1, original.ValueOr(2));
        }   
    }
}