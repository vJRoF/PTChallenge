using System.Numerics;
using EasyNetQ;
using PTChallenge.App1;
using PTChallenge.App1.Models;

namespace PTChallenge.App2;

/// <summary>
///     Отправка чисел для вычисления через рэббит
/// </summary>
public class RabbitSender : INumberSender
{
    private readonly IBus _bus;

    public RabbitSender(IBus bus)
    {
        _bus = bus;
    }
    
    /// <inheritdoc />
    public async Task SendNumberAsync(BigInteger i, CancellationToken ct)
    {
        await _bus.PubSub.PublishAsync(new NumberMessage { Number = $"{i}" }, cancellationToken: ct);
    }
}