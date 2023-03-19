using System.Numerics;

namespace PTChallenge.App1;

/// <summary>
///     Интерфейс для отправки заданий на вычисление в другое приложение
/// </summary>
public interface INumberSender
{
    /// <summary>
    ///     Отправить число в другое приложение для вычисления значения Фибоначчи
    /// </summary>
    /// <param name="i">Число</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task SendNumberAsync(BigInteger i, CancellationToken ct);
}