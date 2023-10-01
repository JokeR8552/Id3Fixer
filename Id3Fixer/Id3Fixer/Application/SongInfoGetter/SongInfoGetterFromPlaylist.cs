using Id3Fixer.Application.Parameters;

namespace Id3Fixer.Application.SongInfoGetter;

public class SongInfoGetterFromPlaylist : ISongInfoGetter
{
    private readonly IArgumentsProvider argumentsProvider;

    public SongInfoGetterFromPlaylist(IArgumentsProvider argumentsProvider)
    {
        this.argumentsProvider = argumentsProvider;
    }

    public List<SongInfo> GetSongInfos()
    {
        var fileReader = File.OpenText(Path.Combine(
            this.argumentsProvider.Parameters.BasePath,
            this.argumentsProvider.Parameters.PlaylistFileName));
        string? line;
        var songLinesStarted = false;
        var songInfos = new List<SongInfo>();
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
                songInfos.Add(new SongInfo(splittedLine[0], splittedLine[1], splittedLine[2], splittedLine[3]));
            }
        }

        return songInfos;
    }
}
