using System;
using alexn.Extensions;
using NUnit.Framework;

namespace alexn.Test
{
    [TestFixture]
    public class DateTimeExtensions
    {
        [Test]
        public void Get_Last_Day_Of_Month()
        {
            Assert.AreEqual(new DateTime(2010, 1, 31), new DateTime(2010, 1, 1).LastDayOfMonth());
        }

        [Test]
        public void Get_First_Day_Of_Month()
        {
            Assert.AreEqual(new DateTime(2010, 1, 1), new DateTime(2010, 1, 16).FirstDayOfMonth());
        }

        [Test]
        public void Get_Midnight()
        {
            Assert.AreEqual(new DateTime(2010, 1, 1, 23, 59, 59), new DateTime(2010, 1, 1).Midnight());
        }

        [Test]
        public void Get_IsInPast() {
            Assert.IsTrue(DateTime.Now.AddDays(-1).IsInPast());
            Assert.IsFalse(DateTime.Now.AddDays(1).IsInPast());
        }

        [Test]
        public void Get_IsInFuture() {
            Assert.IsTrue(DateTime.Now.AddDays(1).IsInFuture());
            Assert.IsFalse(DateTime.Now.AddDays(-1).IsInFuture());
        }
    }
}
