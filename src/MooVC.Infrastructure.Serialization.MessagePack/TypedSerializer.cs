namespace MooVC.Infrastructure.Serialization.MessagePack;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using global::MessagePack;
using MooVC.Compression;

public sealed class TypedSerializer
    : Serializer
{
    public TypedSerializer(
        ICompressor? compressor = default,
        Func<MessagePackSerializerOptions, MessagePackSerializerOptions>? configure = default)
        : base(compressor: compressor, configure: configure, options: MessagePackSerializer.DefaultOptions)
    {
    }

    protected override Task PerformSerializeAsync<T>(T instance, Stream target, CancellationToken? cancellationToken = default)
    {
        return MessagePackSerializer.SerializeAsync(
            target,
            instance,
            options: Options,
            cancellationToken: cancellationToken.GetValueOrDefault());
    }

    protected override ValueTask<T> PerformDeserializeAsync<T>(Stream source, CancellationToken cancellationToken)
    {
        return MessagePackSerializer.DeserializeAsync<T>(source, options: Options, cancellationToken: cancellationToken);
    }
}