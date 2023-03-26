using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTChallenge.App1.MessageHandlers;
using PTChallenge.Common;
using PTChallenge.Common.Calculators;

namespace PTChallenge.App1;

public static class RegisterExtension
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection serviceCollection)
    {
        serviceCollection    
            .AddLogging(logBuilder =>
                logBuilder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace));
        
        serviceCollection.AddTransient<IFibonacciCalculator, FibonacciLoopCalculator>();
        serviceCollection.AddTransient<IApp2Client, App2Client>();
        serviceCollection.AddTransient<INumberSender, RestApiSender>();
        serviceCollection.AddTransient<NumberMessageHandler>();
        serviceCollection.AddSingleton<WorkerPool>();
        serviceCollection.AddTransient<Worker.Factory>();
        serviceCollection.AddMemoryCache();
    
        serviceCollection
            .AddHttpClient(App2Client.Name, client =>
            {
                client.BaseAddress = new Uri("https://localhost:44364/");
            });
    
        serviceCollection.RegisterEasyNetQ("host=localhost", register => register.EnableMicrosoftLogging());
    
        return serviceCollection;
    }
}