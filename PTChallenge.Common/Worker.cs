using System.Diagnostics;
using System.Numerics;
using Microsoft.Extensions.Logging;
using PTChallenge.App1;
using PTChallenge.Common.Calculators;

namespace PTChallenge.Common;

/// <summary>
///     Класс для вычисления числа фибоначчи и отправки его в обратную сторону
/// </summary>
public class Worker
{
    private readonly IFibonacciCalculator _calculator;
    private readonly ILogger<Worker> _logger;
    private readonly INumberSender _sender;
    
    public Worker(
        IFibonacciCalculator calculator,
        ILogger<Worker> logger,
        INumberSender sender)
    {
        _calculator = calculator;
        _logger = logger;
        _sender = sender;
    }
    
    public async Task StartAsync(BigInteger n, CancellationToken ct)
    {
        await _sender.SendNumberAsync(n, ct);
    }

    public async Task CalculateAndSendAsync(BigInteger i, CancellationToken ct)
    {
        var stopwotch = new Stopwatch();
        stopwotch.Start();
        
        _logger.LogInformation(6541882, "Получено число для вычисления: {Number}", i);
        
        var result = _calculator.Calculate(i);
        await _sender.SendNumberAsync(i + 1, ct);
        
        _logger.LogInformation(4289293, "Значение {Result} вычислено за {Time}", result, stopwotch.Elapsed);
    }
}