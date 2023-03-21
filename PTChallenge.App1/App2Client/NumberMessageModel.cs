using System.Text.Json.Serialization;

namespace PTChallenge.App1;

/// <summary>
///     Модель для отправки запроса на вычисление по Rest API
/// </summary>
public class NumberMessageModel
{
    [JsonPropertyName("number")]
    public string Number { get; init; } = default!;
}