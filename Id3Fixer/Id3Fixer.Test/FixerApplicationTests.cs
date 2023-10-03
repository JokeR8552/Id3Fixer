using Id3Fixer.Application;
using Id3Fixer.Application.SongInfoGetter;
using Id3Fixer.Application.TagsFixer;
using Moq;

namespace Id3Fixer.Test;

[TestFixture]
public class FixerApplicationTests
{
    private readonly Mock<ITagsFixer> _tagsFixerMock = new();
    private readonly Mock<ISongInfoGetter> _songInfoGetterMock = new();

    [Test]
    public void Run_positive()
    {
        var app = new FixerApplication(_songInfoGetterMock.Object, _tagsFixerMock.Object);

        app.Run();

        _songInfoGetterMock.Verify(g => g.GetSongInfos(), Times.Once);
        _tagsFixerMock.Verify(f => f.FixTags(It.IsAny<List<SongInfo>>()), Times.Once);
    }
}
