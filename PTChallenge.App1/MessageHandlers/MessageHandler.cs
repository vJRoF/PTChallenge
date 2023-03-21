namespace PTChallenge.App1.MessageHandlers;

internal interface IMessageHandler<in T>
{
     Task HandleAsync(T message, CancellationToken ct);
}