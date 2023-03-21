using System.Numerics;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTChallenge.App1;
using PTChallenge.App1.MessageHandlers;
using PTChallenge.Common;
using PTChallenge.Common.Models;

var serviceCollection = new ServiceCollection()
    .RegisterAppServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

var bus = serviceProvider.GetService<IBus>();
bus!.PubSub.Subscribe<NumberMessage>(
    subscriptionId: $"{Guid.NewGuid()}", 
    onMessage: async msg =>
        await serviceProvider.GetService<NumberMessageHandler>()!
            .HandleAsync(msg, CancellationToken.None));

var worker = serviceProvider.GetService<Worker>();
await worker!.StartAsync(BigInteger.Parse(args[0]), CancellationToken.None);

var logger = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger<Program>();
logger.LogInformation("Приложение запущено, нажмите Enter для остановки");
Console.ReadLine();
logger.LogInformation("Приложение остановлено");