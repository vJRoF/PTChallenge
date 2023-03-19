using System.Text.Json.Serialization;

namespace PTChallenge.App1.App2Client;

/// <summary>
///     Модель для отправки запроса на вычисление по Rest API
/// </summary>
public class NumberMessageModel
{
    [JsonPropertyName("number")]
    public string Number { get; set; } = default!;
}