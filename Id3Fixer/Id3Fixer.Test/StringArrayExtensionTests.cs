using Id3Fixer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Id3Fixer.Test
{
    [TestFixture]
    internal class StringArrayExtensionTests
    {
        [Test]
        public void GetSafe_Positive()
        {
            var array = new string[] { "1", "2" };
            Assert.Multiple(() =>
            {
                Assert.That(array.GetSafe(0), Is.EqualTo("1"));
                Assert.That(array.GetSafe(1), Is.EqualTo("2"));
                Assert.That(array.GetSafe(2), Is.EqualTo(null));
            });
        }
    }
}
