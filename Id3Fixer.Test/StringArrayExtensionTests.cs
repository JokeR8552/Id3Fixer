using Id3Fixer.Extensions;

namespace Id3Fixer.Test;

[TestFixture]
internal class StringArrayExtensionTests
{
    [Test]
    public void GetSafe_Positive()
    {
        string[] array = new string[] { "1", "2" };

        Assert.Multiple(() =>
        {
            Assert.That(array.GetSafe(0), Is.EqualTo("1"));
            Assert.That(array.GetSafe(1), Is.EqualTo("2"));
            Assert.That(array.GetSafe(2), Is.EqualTo(null));
        });
    }
}
