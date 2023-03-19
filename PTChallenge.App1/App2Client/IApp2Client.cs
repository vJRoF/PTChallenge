using PTChallenge.Common.Models;

namespace PTChallenge.App1.App2Client;

public interface IApp2Client
{
    Task Calculate(NumberMessageModel message, CancellationToken ct);
}