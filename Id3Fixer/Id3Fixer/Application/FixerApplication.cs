using Id3;
using Id3Fixer.Application.Parameters;
using Id3Fixer.Application.SongInfoGetter;
using Id3Fixer.Application.TagsFixer;

namespace Id3Fixer.Application;

public class FixerApplication : IApplication
{
    private readonly ISongInfoGetter _songInfoGetter;
    private readonly ITagsFixer _tagsFixer;

    public FixerApplication(
        ISongInfoGetter songInfoGetter,
        ITagsFixer tagsFixer)
    {
        _songInfoGetter = songInfoGetter;
        _tagsFixer = tagsFixer;
    }

    public void Run()
    {
        List<SongInfo> songInfos = _songInfoGetter.GetSongInfos();

        _tagsFixer.FixTags(songInfos);
    }
}
