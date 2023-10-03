using Id3;
using Id3Fixer.Application.Parameters;

namespace Id3Fixer.Application.TagsFixer;

public class TagsFixer : ITagsFixer
{
    private readonly IArgumentsProvider _argumentProvider;

    public TagsFixer(IArgumentsProvider argumentProvider)
    {
        _argumentProvider = argumentProvider;
    }

    public void FixTags(List<SongInfo> songInfos)
    {
        string basePath = _argumentProvider.Parameters.BasePath;
        foreach (SongInfo songInfo in songInfos)
        {
            string mp3Path = Path.Combine(basePath, songInfo.Path);
            if (!File.Exists(mp3Path))
            {
                continue;
            }

            try
            {
                var mp3 = new Mp3(mp3Path, Mp3Permissions.ReadWrite);
                Id3Tag? tag2 = mp3.GetTag(Id3TagFamily.Version2X);
                tag2 ??= GetTag2FromTag1(mp3);

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
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(mp3Path);
                Console.WriteLine(ex);
                Console.WriteLine();
                continue;
            }
        }
    }

    private static Id3Tag GetTag2FromTag1(Mp3 mp3)
    {
        Id3Tag tag1 = mp3.GetTag(Id3TagFamily.Version1X);

        return tag1.ConvertTo(Id3Version.V23);
    }
}
