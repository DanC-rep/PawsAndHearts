using System.Threading.Channels;
using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Infrastructure.MessageQueues;

public class FilesCleanerMessageQueue<TMessage> : IMessageQueue<TMessage>
{
    private readonly Channel<TMessage> _channel = 
        Channel.CreateUnbounded<TMessage>();

    public async Task WriteAsync(
        TMessage paths, 
        CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(paths, cancellationToken);
    }

    public async Task<TMessage> ReadAsync(CancellationToken cancellationToken = default)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}