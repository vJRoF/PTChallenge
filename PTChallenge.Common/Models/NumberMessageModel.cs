using System.Text.Json.Serialization;

namespace PTChallenge.Common.Models;

/// <summary>
///     Модель для отправки запроса на вычисление по Rest API
/// </summary>
public class NumberMessageModel
{
    [JsonPropertyName("chainId")]
    public string ChainId { get; init; } = default!;
    
    [JsonPropertyName("number")]
    public string Number { get; init; } = default!;
}