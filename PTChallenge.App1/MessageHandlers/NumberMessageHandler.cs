using System.Numerics;
using PTChallenge.Common;
using PTChallenge.Common.Models;

namespace PTChallenge.App1.MessageHandlers;

public class NumberMessageHandler : IMessageHandler<NumberMessage>
{
    private readonly Worker _worker;

    public NumberMessageHandler(Worker worker)
    {
        _worker = worker;
    }

    public async Task HandleAsync(NumberMessage message, CancellationToken ct)
    {
        var bigIntegerNumber = BigInteger.Parse(message.Number);
        await _worker.CalculateAndSendAsync(bigIntegerNumber, ct);
    }
}