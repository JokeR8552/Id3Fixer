using Id3Fixer.Application.Parameters;

namespace Id3Fixer.Application.SongInfoGetter;

public class SongInfoGetterFromPlaylist : ISongInfoGetter
{
    private readonly IArgumentsProvider _argumentsProvider;

    public SongInfoGetterFromPlaylist(IArgumentsProvider argumentsProvider)
    {
        _argumentsProvider = argumentsProvider;
    }

    public List<SongInfo> GetSongInfos()
    {
        StreamReader fileReader = File.OpenText(Path.Combine(
            _argumentsProvider.Parameters.BasePath,
            _argumentsProvider.Parameters.PlaylistFileName));
        string? line;
        bool songLinesStarted = false;
        List<SongInfo> songInfos = new();
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
                string[] splittedLine = line.Split('|');
                songInfos.Add(new SongInfo(splittedLine[0], splittedLine[1], splittedLine[2], splittedLine[3]));
            }
        }

        return songInfos;
    }
}
