using Id3Fixer.Extensions;
namespace Id3Fixer.Application.Parameters;

public class ConsoleAppArgumentsProvider : IArgumentsProvider
{
    public const string DefaultPath = "C:\\Users\\imukhametov\\Documents\\My\\fix\\music";
    public const string DefaultPlaylist = "ЖИТЬ.aimppl4";

    public ConsoleAppArgumentsProvider(string[] args)
    {
        Parameters = new Parameters(
             args.GetSafe(0) ?? DefaultPath,
             args.GetSafe(1) ?? DefaultPlaylist);
    }

    public Parameters Parameters { get; }
}
