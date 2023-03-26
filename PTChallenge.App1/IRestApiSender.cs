using PTChallenge.Common;
using PTChallenge.Common.Models;

namespace PTChallenge.App1;

/// <summary>
///     Отправка сообщения с числом в приложение с REST интерфейсом
/// </summary>
public class RestApiSender : INumberSender
{
    private readonly IApp2Client _app2Client;

    public RestApiSender(IApp2Client app2Client)
    {
        _app2Client = app2Client;
    }
    
    /// <inheritdoc />
    public async Task SendNumberAsync(NumberMessage message, CancellationToken ct)
    {
        var numberMessageModel = new NumberMessageModel
        {
            ChainId = message.ChainId,
            Number = $"{message.Number}"
        };
        await _app2Client.SendNumber(numberMessageModel, ct);
    }
}