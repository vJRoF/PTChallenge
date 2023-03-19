using System.Numerics;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTChallenge.App1;
using PTChallenge.App1.App2Client;
using PTChallenge.App1.Models;
using PTChallenge.Common;
using PTChallenge.Common.Calculators;

var serviceCollection = new ServiceCollection();
RegisterAppServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();

var worker = serviceProvider.GetService<Worker>();
var bus = serviceProvider.GetService<IBus>();
bus.PubSub.Subscribe<NumberMessage>($"{Guid.NewGuid()}", async msg =>
{
    var worker = serviceProvider.GetService<Worker>();
    var bigIntegerNumber = BigInteger.Parse(msg.Number);
    await worker.CalculateAndSendAsync(bigIntegerNumber, CancellationToken.None);
});
await worker!.StartAsync(BigInteger.Parse(args[0]), CancellationToken.None);

var logger = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger<Program>();
logger.LogInformation("Приложение запущено, нажмите Enter для остановки");
Console.ReadLine();
logger.LogInformation("Приложение остановлено");

static IServiceCollection RegisterAppServices(IServiceCollection serviceCollection)
{
    serviceCollection    
        .AddLogging(logBuilder =>
            logBuilder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Trace));
    
    serviceCollection.AddTransient<Worker>();
    serviceCollection.AddTransient<IFibonacciCalculator, FibonacciLoopCalculator>();
    serviceCollection.AddTransient<IApp2Client, App2Client>();
    serviceCollection.AddTransient<INumberSender, RestApiSender>();
    
    serviceCollection
        .AddHttpClient(App2Client.Name, client =>
        {
            client.BaseAddress = new Uri("https://localhost:44364/");
        });
    
    serviceCollection.RegisterEasyNetQ("host=localhost", register => register.EnableMicrosoftLogging());
    
    return serviceCollection;
}