using PTChallenge.Common.Models;

namespace PTChallenge.Common;

/// <summary>
///     Интерфейс для отправки заданий на вычисление в другое приложение
/// </summary>
public interface INumberSender
{
    /// <summary>
    ///     Отправить число в другое приложение для вычисления значения Фибоначчи
    /// </summary>
    /// <param name="message"><see cref="NumberMessage"/></param>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    Task SendNumberAsync(NumberMessage message, CancellationToken ct);
}