using Id3;
using Id3Fixer.Extensions;

namespace Id3Fixer;

public class FixerProgram
{ 
    public static void Main(string[] args)
    {
        var basePath = args.GetSafe(0) ?? "C:\\Users\\imukhametov\\Documents\\My\\fix\\music";
        var playlistFileName = args.GetSafe(1) ?? "ЖИТЬ.aimppl4";

        var fileReader = File.OpenText(Path.Combine(basePath, playlistFileName));
        string? line;
        var songLinesStarted = false;
        var songInfos = new List<(string Path, string Name, string Artist, string Album)>();
        while (true)
        {
            line = fileReader.ReadLine();
            if (line is null)
            {
                break;
            }

            if (line == "#-----CONTENT-----#")
            {
                songLinesStarted = true;
                continue;
            }

            if (songLinesStarted && !string.IsNullOrWhiteSpace(line))
            {
                var splittedLine = line.Split('|');
                songInfos.Add(new(splittedLine[0], splittedLine[1], splittedLine[2], splittedLine[3]));
            }
        }

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