using System.Linq;
using alexn.Extensions;
using NUnit.Framework;

namespace alexn.Test
{
    [TestFixture]
    public class CollectionExtensions
    {
        [Test]
        public void Can_Shuffle()
        {
            var items = new[] {"Foo", "Bar", "Baz", "Boo", "Alexander", "Nyquist"};
            var shuffled = items.Shuffle();

            Assert.IsFalse(items.SequenceEqual(shuffled));
        }
    }
}