using Id3Fixer.Application;
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
    public void GetSongInfos_Positive_OneRow()
    {
        argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters("Files", "play.aimppl4"));
        var getter = new SongInfoGetterFromPlaylist(argumentsProviderMock.Object);

        List<SongInfo> infos = getter.GetSongInfos();

        Assert.That(infos, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(infos[0].Name, Is.EqualTo("Дружок"));
            Assert.That(infos[0].Artist, Is.EqualTo("Жить"));
            Assert.That(infos[0].Album, Is.EqualTo("Не Святая"));
        });
    }

    [Test]
    public void GetSongInfos_Positive_ThreeRows()
    {
        argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters("Files", "play_threeRows.aimppl4"));
        var getter = new SongInfoGetterFromPlaylist(argumentsProviderMock.Object);

        List<SongInfo> infos = getter.GetSongInfos();

        Assert.That(infos, Has.Count.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(infos[0].Artist, Is.EqualTo("Жить1"));
            Assert.That(infos[0].Name, Is.EqualTo("Дружок1"));
            Assert.That(infos[0].Album, Is.EqualTo("Не Святая1"));
            Assert.That(infos[1].Artist, Is.EqualTo("Жить2"));
            Assert.That(infos[1].Name, Is.EqualTo("Дружок2"));
            Assert.That(infos[1].Album, Is.EqualTo("Не Святая2"));
            Assert.That(infos[2].Artist, Is.EqualTo("Жить3"));
            Assert.That(infos[2].Name, Is.EqualTo("Дружок3"));
            Assert.That(infos[2].Album, Is.EqualTo("Не Святая3"));
        });
    }

    [Test]
    public void GetSongInfos_Throws_OnNoFile()
    {
        argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters("Files", "NotPresent"));
        var getter = new SongInfoGetterFromPlaylist(argumentsProviderMock.Object);

        Assert.Throws<FileNotFoundException>(() => getter.GetSongInfos());
    }
}
