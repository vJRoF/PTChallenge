using System.Numerics;
using EasyNetQ;
using PTChallenge.Common;
using PTChallenge.Common.Models;

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
    public async Task SendNumberAsync(NumberMessage message, CancellationToken ct)
    {
        var numberMessageModel = new NumberMessageModel
        {
            ChainId = message.ChainId,
            Number = $"{message.Number}"
        };
        await _bus.PubSub.PublishAsync(numberMessageModel, cancellationToken: ct);
    }
}