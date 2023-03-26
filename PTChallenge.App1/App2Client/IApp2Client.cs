using PTChallenge.Common.Models;

namespace PTChallenge.App1;

public interface IApp2Client
{
    Task SendNumber(NumberMessageModel message, CancellationToken ct);
}