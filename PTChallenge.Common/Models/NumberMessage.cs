using System.Numerics;

namespace PTChallenge.Common.Models;

/// <summary>
///     Сообщение с числом последовательности
/// </summary>
public class NumberMessage
{
    /// <summary>
    ///     Идентификатор рассчитываемой последовательности
    /// </summary>
    public string ChainId { get; init; } = default!;
    
    /// <summary>
    ///     Очередное число последовательности
    /// </summary>
    public BigInteger Number { get; init; } = default!;
}