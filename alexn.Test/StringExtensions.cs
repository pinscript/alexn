using alexn.Extensions;
using NUnit.Framework;

namespace alexn.Test
{
    [TestFixture]
    public class StringExtensions
    {
        [Test]
        public void ToSlug()
        {
            var url = " http://www.mediaanalys.se ";
            Assert.AreEqual("http-www-mediaanalys-se", url.ToSlug());
        }

        [Test]
        public void Md5()
        {
            Assert.AreEqual("acbd18db4cc2f85cedef654fccc4a4d8", "foo".Md5());
        }
    }
}