using Id3;
using Id3Fixer.Application.Parameters;
using Id3Fixer.Application.SongInfoGetter;

namespace Id3Fixer.Application;

public class FixerApplication : IApplication
{
    private readonly IArgumentsProvider argumentsProvider;
    private readonly ISongInfoGetter songInfoGetter;

    public FixerApplication(
        IArgumentsProvider argumentsProvider,
        ISongInfoGetter songInfoGetter) 
    {
        this.argumentsProvider = argumentsProvider;
        this.songInfoGetter = songInfoGetter;
    }

    public void Run()
    {
        var basePath = this.argumentsProvider.Parameters.BasePath;
        List<SongInfo> songInfos = songInfoGetter.GetSongInfos();

        FixTags(basePath, songInfos);
    }

    private static void FixTags(string basePath, List<SongInfo> songInfos)
    {
        foreach (var songInfo in songInfos)
        {
            var mp3Path = Path.Combine(basePath, songInfo.Path);
            var mp3 = new Mp3(mp3Path, Mp3Permissions.ReadWrite);
            var tag2 = mp3.GetTag(Id3TagFamily.Version2X);

            tag2.Album = songInfo.Album;
            tag2.Title = songInfo.Name;
            var artist = new Id3.Frames.ArtistsFrame();
            artist.Value.Add(songInfo.Artist);
            tag2.Artists = artist;

            tag2.Album.EncodingType = Id3TextEncoding.Unicode;
            tag2.Title.EncodingType = Id3TextEncoding.Unicode;
            tag2.Artists.EncodingType = Id3TextEncoding.Unicode;

            mp3.DeleteTag(Id3TagFamily.Version1X);
            mp3.WriteTag(tag2);
            mp3.Dispose();
        }
    }
}
