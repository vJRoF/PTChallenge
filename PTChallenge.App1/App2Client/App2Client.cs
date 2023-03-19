using System.Numerics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace PTChallenge.App1;

public class App2Client : IApp2Client
{
    public const string Name = nameof(App2Client);
    private HttpClient _client;

    public App2Client(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient(Name);
    }

    public async Task Calculate(BigInteger i, CancellationToken ct)
    {
        var response = await _client.GetAsync($"Fibonacci?i={i}", ct);
        if (response.IsSuccessStatusCode)
            return;
        else
        {
            var responseStream = await response.Content.ReadAsStreamAsync(ct);
            var problemDetails = await JsonSerializer.DeserializeAsync<ProblemDetails>(responseStream, cancellationToken: ct);
            throw new InvalidOperationException(problemDetails.Detail);
        }
    }
}