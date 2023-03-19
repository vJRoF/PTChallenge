using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTChallenge.App1;

//setup our DI
var serviceCollection = new ServiceCollection()
    .AddLogging(logBuilder =>
        logBuilder
            .AddConsole()
            .SetMinimumLevel(LogLevel.Trace))
    .AddTransient<Worker>()
    .AddTransient<IApp2Client, App2Client>();


serviceCollection
    .AddHttpClient(App2Client.Name, client =>
    {
        client.BaseAddress = new Uri("https://localhost:44364/");
    });

var serviceProvider = serviceCollection.BuildServiceProvider();

var worker = serviceProvider.GetService<Worker>();
await worker.Start(BigInteger.Parse(args[0]));

var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
logger.LogInformation("Приложение запущено, нажмите Enter для остановки");
Console.ReadLine();
logger.LogInformation("Приложение остановлено");