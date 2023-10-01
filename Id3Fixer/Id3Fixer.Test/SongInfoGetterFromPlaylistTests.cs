using Id3Fixer.Application.Parameters;
using Id3Fixer.Application.SongInfoGetter;
using Moq;

namespace Id3Fixer.Test;

[TestFixture]
internal class SongInfoGetterFromPlayListTests
{
    private Mock<IArgumentsProvider> argumentsProviderMock;
    [SetUp]
    public void SetUp()
    {
        argumentsProviderMock = new Mock<IArgumentsProvider>();
    }

    [Test]
    public void GetSongInfos_Positive()
    {
        argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters("Files", "play.aimppl4"));
        var getter = new SongInfoGetterFromPlaylist(argumentsProviderMock.Object);

        var infos = getter.GetSongInfos();
        Assert.That(infos.Count, Is.EqualTo(1));
        Assert.That(infos[0].Artist, Is.EqualTo("Жить"));
        argumentsProviderMock.Verify(p => p.Parameters, Times.Exactly(2));
    }

    [Test]
    public void GetSongInfos_Positive2()
    {
        argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters("Files", "play1.aimppl4"));
        var getter = new SongInfoGetterFromPlaylist(argumentsProviderMock.Object);

        var infos = getter.GetSongInfos();
        Assert.That(infos.Count, Is.EqualTo(3));
        Assert.That(infos[0].Artist, Is.EqualTo("Жить"));
        Assert.That(infos[0].Name, Is.EqualTo("Дружок"));
        Assert.That(infos[1].Name, Is.EqualTo("Дружок1"));
        Assert.That(infos[2].Name, Is.EqualTo("Дружок2"));
        argumentsProviderMock.Verify(p => p.Parameters, Times.Exactly(2));
    }

    [Test]
    public void GetSongInfos_Throws_OnNoFile()
    {
        argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters("Files", "NotPresent"));
        var getter = new SongInfoGetterFromPlaylist(argumentsProviderMock.Object);

        Assert.Throws<FileNotFoundException>(() => getter.GetSongInfos());
        argumentsProviderMock.Verify(p => p.Parameters, Times.Exactly(2));
    }
}
