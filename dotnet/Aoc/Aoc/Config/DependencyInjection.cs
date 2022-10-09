using System;
using Aoc.Domain;
using Aoc.Domain.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Aoc.Config;

public static class DependencyInjection
{
    public static ILogger Logger = null;
    public static void RegisterLogger(ILogger logger)
    {
        Logger = logger;
    }

    //This property is set up for unit tests. When unit tests place a value here the services are used instead of production's.
    public static IServiceCollection RegisteredServiceCollection { get; set; }

    public static IServiceProvider GetServiceProvider()
    {
        if (RegisteredServiceCollection != null)
        {
            RegisteredServiceCollection.AddSingleton(Logger);
            return RegisteredServiceCollection.BuildServiceProvider();
        }
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBaseServices();
        return serviceCollection.BuildServiceProvider();
    }

    private static void AddBaseServices(this IServiceCollection serviceCollection)
    {
        RegisterLogger(new Logger());
        serviceCollection.AddSingleton(Logger);
        serviceCollection.AddSingleton<IAdder, Adder>();
    }
}