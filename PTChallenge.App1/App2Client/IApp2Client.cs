using System.Numerics;

namespace PTChallenge.App1;

public interface IApp2Client
{
    Task Calculate(BigInteger i, CancellationToken ct);
}