using Id3Fixer.Application;
using Id3Fixer.Application.Parameters;
using Id3Fixer.Application.SongInfoGetter;
using Id3Fixer.Application.TagsFixer;
using Microsoft.Extensions.DependencyInjection;

namespace Id3Fixer;

public class FixerProgram
{
    public static void Main(string[] args)
    {
        ServiceProvider serviceProvider = SetupServiceProvider(args);
        IApplication app = serviceProvider.GetRequiredService<IApplication>();

        app.Run();
    }

    public static ServiceProvider SetupServiceProvider(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IApplication, FixerApplication>();
        serviceCollection.AddTransient<IArgumentsProvider, ConsoleAppArgumentsProvider>((_) => new ConsoleAppArgumentsProvider(args));
        serviceCollection.AddTransient<ISongInfoGetter, SongInfoGetterFromPlaylist>();
        serviceCollection.AddTransient<ITagsFixer, TagsFixer>();

        return serviceCollection.BuildServiceProvider();
    }
}