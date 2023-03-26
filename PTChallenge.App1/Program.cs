using System.Numerics;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTChallenge.App1;
using PTChallenge.App1.MessageHandlers;
using PTChallenge.Common;
using PTChallenge.Common.Models;

// валидация входных параметров
if (args.Length != 1)
    throw new ArgumentException("Неверное число аргументов. Должно быть задано одно число:" +
                                "- количество параллельных расчётов");
if (!int.TryParse(args[0], out var dop))
    throw new ArgumentException($"Неправильно задано количество параллельных расчётов: {args[0]}");

// создание контейнера и регистрация зависимостей
var serviceCollection = new ServiceCollection()
    .RegisterAppServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

// подписка на сообщения
var bus = serviceProvider.GetService<IBus>();
bus!.PubSub.Subscribe<NumberMessageModel>(
    subscriptionId: $"{Guid.NewGuid()}", 
    onMessage: async msg =>
        await serviceProvider.GetService<NumberMessageHandler>()!
            .HandleAsync(msg, CancellationToken.None));

var workerPool = serviceProvider.GetService<WorkerPool>()!;
// запуск параллельных цепочек расчётов
for (var i = 0; i < dop; i++)
{
    var worker = workerPool.Create();
    await worker.StartAsync(CancellationToken.None);
}


var logger = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger<Program>();
logger.LogInformation("Приложение запущено, нажмите Enter для остановки");
Console.ReadLine();
logger.LogInformation("Приложение остановлено");