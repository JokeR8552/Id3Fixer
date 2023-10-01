using Id3Fixer.Application.Parameters;

namespace Id3Fixer.Test
{
    [TestFixture]
    internal class ConsoleAppArgumentsProviderTests
    {
        [Test]
        public void Parameters_Positive()
        {
            var provider = new ConsoleAppArgumentsProvider(new string[] { "p", "fn", "notNeeded" });
            Assert.Multiple(() =>
            {
                Assert.That(provider.Parameters.BasePath, Is.EqualTo("p"));
                Assert.That(provider.Parameters.PlaylistFileName, Is.EqualTo("fn"));
            });
        }

        [Test]
        public void Parameters_DefaultValuesOnEmptyArray()
        {
            var provider = new ConsoleAppArgumentsProvider(Array.Empty<string>());
            Assert.Multiple(() =>
            {
                Assert.That(provider.Parameters.BasePath, Is.EqualTo(ConsoleAppArgumentsProvider.DefaultPath));
                Assert.That(provider.Parameters.PlaylistFileName, Is.EqualTo(ConsoleAppArgumentsProvider.DefaultPlaylist));
            });
        }
    }
}
