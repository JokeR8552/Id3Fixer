using Id3;
using Id3Fixer.Application;
using Id3Fixer.Application.Parameters;
using Id3Fixer.Application.TagsFixer;
using Moq;

namespace Id3Fixer.Test;

[TestFixture]
internal class TagsFixerTests
{
    private const string FilesDir = "Files";
    private readonly SongInfo _expectedInfo = new("work.mp3", "name1", "artist1", "album1");
    private readonly string _basePath = Path.Combine(Environment.CurrentDirectory, FilesDir);

    private readonly Mock<IArgumentsProvider> _argumentsProviderMock = new();

    [Test]
    public void FixTags_Positive()
    {
        File.Replace(
            Path.Combine(_basePath, "bad_backup.mp3"),
            Path.Combine(_basePath, "work.mp3"), null);
        _argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters(FilesDir));
        var fixer = new TagsFixer(_argumentsProviderMock.Object);
        List<SongInfo> songInfos = new()
        {
            new SongInfo("NonExistentFile1"),
            _expectedInfo,
            new SongInfo("NonExistentFile2")
        };

        Assert.DoesNotThrow(() => fixer.FixTags(songInfos));

        var mp3work = new Mp3(Path.Combine(_basePath, "work.mp3"), Mp3Permissions.Read);
        Id3Tag tagsWork = mp3work.GetTag(Id3TagFamily.Version2X);
        Assert.Multiple(() =>
        {
            Assert.That(tagsWork.Album.Value, Is.EqualTo(_expectedInfo.Album));
            Assert.That(string.Join(string.Empty, tagsWork.Artists.Value), Is.EqualTo(_expectedInfo.Artist));
            Assert.That(tagsWork.Title.Value, Is.EqualTo(_expectedInfo.Name));
        });
        mp3work.Dispose();
    }
}
