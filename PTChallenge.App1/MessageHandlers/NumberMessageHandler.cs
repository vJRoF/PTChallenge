using System.Numerics;
using PTChallenge.Common;
using PTChallenge.Common.Models;

namespace PTChallenge.App1.MessageHandlers;

public class NumberMessageHandler : IMessageHandler<NumberMessageModel>
{
    private readonly WorkerPool _workerPool;

    public NumberMessageHandler(WorkerPool workerPool)
    {
        _workerPool = workerPool;
    }

    public async Task HandleAsync(NumberMessageModel message, CancellationToken ct)
    {
        var worker = _workerPool.GetOrCreate(message.ChainId);
        var number = BigInteger.Parse(message.Number);
        await worker.CalculateAndSendAsync(number, ct);
    }
}