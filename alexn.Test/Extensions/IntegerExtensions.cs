using System;
using alexn.Extensions;
using NUnit.Framework;

namespace alexn.Test
{
    [TestFixture]
    public class IntegerExtensions
    {
        [Test]
        public void Timestamp_To_DateTime()
        {
            Assert.AreEqual(1262344953, new DateTime(2010, 1, 1, 11, 22, 33).Timestap());
        }

        [Test]
        public void To_Month_And_Year()
        {
            Assert.AreEqual(new DateTime(2010, 1, 3), 3.January(2010));
        }

        [Test]
        public void Times_Runs_Correct()
        {
            var i = 0;
            2.Times(() => i++);
            Assert.AreEqual(2, i);
        }
    }
}