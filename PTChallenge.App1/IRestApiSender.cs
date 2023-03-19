using System.Numerics;
using PTChallenge.App1.App2Client;
using PTChallenge.Common.Models;

namespace PTChallenge.App1;

public class RestApiSender : INumberSender
{
    private readonly IApp2Client _app2Client;

    public RestApiSender(IApp2Client app2Client)
    {
        _app2Client = app2Client;
    }
    
    public async Task SendNumberAsync(BigInteger i, CancellationToken ct)
    {
        await _app2Client.Calculate(new NumberMessageModel {Number = $"{i}"}, ct);
    }
}