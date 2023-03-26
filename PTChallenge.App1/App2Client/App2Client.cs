using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PTChallenge.Common.Models;

namespace PTChallenge.App1;

/// <summary>
///     Клиент для доступа к REST API приложения 2
/// </summary>
public class App2Client : IApp2Client
{
    public const string Name = nameof(App2Client);
    private HttpClient _client;

    public App2Client(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient(Name);
    }

    /// <summary>
    ///     Отправить число на вычисление
    /// </summary>
    /// <param name="message">Сообщение для отпрвки</param>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task SendNumber(NumberMessageModel message, CancellationToken ct)
    {
        var response = await _client.PostAsync("api/fibonacci/calculate", JsonContent.Create(message), ct);
        if (response.IsSuccessStatusCode)
            return;
        else
        {
            var responseStream = await response.Content.ReadAsStreamAsync(ct);
            var problemDetails = await JsonSerializer.DeserializeAsync<ProblemDetails>(responseStream, cancellationToken: ct);
            throw new InvalidOperationException(problemDetails?.Detail ?? "Неопределённая ошибка");
        }
    }
}