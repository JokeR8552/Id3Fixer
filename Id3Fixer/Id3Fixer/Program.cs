using Id3;
using Id3Fixer.Application;
using Id3Fixer.Application.Parameters;
using Id3Fixer.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Id3Fixer;

public class FixerProgram
{ 
    public static void Main(string[] args)
    {
        var serviceProvider = SetupServiceProvider(args);

        var app = serviceProvider.GetRequiredService<IApplication>();
        app.Run();
    }

    public static ServiceProvider SetupServiceProvider(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IApplication, FixerApplication>();
        serviceCollection.AddTransient<IArgumentsProvider, ConsoleAppArgumentsProvider>((_) => new ConsoleAppArgumentsProvider(args));

        return serviceCollection.BuildServiceProvider();
    }
}