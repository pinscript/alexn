using System;
using NUnit.Framework;

namespace alexn.Test
{
    [TestFixture]
    public class GuardAgainst
    {
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Throws_For_Null()
        {
            Guard.Against.Null(null);
        }

        [Test]
        public void Null_Does_Not_Throw_For_Empty_String()
        {
            Guard.Against.Null("");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void NullOrEmpty_Throws_For_Empty_String()
        {
            Guard.Against.NullOrEmpty("");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void NullOrEmpty_Throws_For_Empty_List()
        {
            Guard.Against.NullOrEmpty(new string[] {});
        }

        [Test]
        public void NullOrEmpty_Does_Not_Throw_For_List()
        {
            Guard.Against.NullOrEmpty(new[] { "alexander", "nyquist" });
        }
    }
}