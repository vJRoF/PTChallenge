﻿using System.Numerics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace PTChallenge.App1.App2Client;

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
    /// <param name="i">Значние числа</param>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task Calculate(BigInteger i, CancellationToken ct)
    {
        var response = await _client.GetAsync($"Fibonacci?i={i}", ct);
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