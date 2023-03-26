using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTChallenge.Common.Models;

namespace PTChallenge.Common;

/// <summary>
///     Класс для вычисления числа фибоначчи и отправки его в обратную сторону
/// </summary>
public class Worker
{
    private BigInteger _previousNumber = 0;
    private readonly ILogger<Worker> _logger;
    private readonly INumberSender _sender;

    public string ChainId { get; }
    public string WorkerId { get; } = Guid.NewGuid().ToString("N");
    
    private Worker(
        ILogger<Worker> logger,
        INumberSender sender,
        string chainId)
    {
        _logger = logger;
        _sender = sender;
        ChainId = chainId;
    }

    private Worker(
        ILogger<Worker> logger,
        INumberSender sender)
        : this (logger, sender, $"{Guid.NewGuid():N}"){}
    
    public async Task StartAsync(CancellationToken ct)
    {
        var initialMessage = new NumberMessage
        {
            ChainId = ChainId,
            Number = 1 // N(0) = 1
        };
        
        _previousNumber = initialMessage.Number;
        await _sender.SendNumberAsync(initialMessage, ct);
    }

    public async Task CalculateAndSendAsync(BigInteger n, CancellationToken ct)
    {
        var currentNumber = _previousNumber + n;
        _logger.LogInformation(50935093, "Рассчитано следующеей число последовательности {Number}", currentNumber);
        
        var messageToSend = new NumberMessage
        {
            ChainId = ChainId,
            Number = currentNumber
        };
        
        _previousNumber = currentNumber;
        await _sender.SendNumberAsync(messageToSend, ct);
    }

    public class Factory
    {
        private readonly IServiceProvider _serviceProvider;

        public Factory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Worker Create()
        {
            var logger = _serviceProvider.GetService<ILoggerFactory>()!.CreateLogger<Worker>();
            var numberSender = _serviceProvider.GetService<INumberSender>()!;
            return new Worker(logger, numberSender);
        }
        
        public Worker Create(string chainId)
        {
            var logger = _serviceProvider.GetService<ILoggerFactory>()!.CreateLogger<Worker>();
            var numberSender = _serviceProvider.GetService<INumberSender>()!;
            return new Worker(logger, numberSender, chainId);
        }
    }
}