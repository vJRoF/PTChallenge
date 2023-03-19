using System.Numerics;

namespace PTChallenge.App1;

public class Worker
{
    private readonly IApp2Client _client;
    
    public Worker(IApp2Client client)
    {
        _client = client;
    }
    
    public async Task Start(BigInteger n)
    {
        await _client.Calculate(n, CancellationToken.None);
    }
}