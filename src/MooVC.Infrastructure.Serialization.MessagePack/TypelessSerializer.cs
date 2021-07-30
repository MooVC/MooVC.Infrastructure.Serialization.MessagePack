namespace MooVC.Infrastructure.Serialization.MessagePack
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using global::MessagePack;
    using MooVC.Compression;

    public sealed class TypelessSerializer
        : Serializer
    {
        public TypelessSerializer(
            ICompressor? compressor = default,
            Func<MessagePackSerializerOptions, MessagePackSerializerOptions>? configure = default)
            : base(
                  compressor: compressor,
                  configure: configure,
                  options: MessagePackSerializer.Typeless.DefaultOptions)
        {
        }

        protected override Task PerformSerializeAsync<T>(
            T instance,
            Stream target,
            CancellationToken? cancellationToken = default)
        {
            return MessagePackSerializer.Typeless.SerializeAsync(
                target,
                instance,
                options: Options,
                cancellationToken: cancellationToken.GetValueOrDefault());
        }

        protected override async ValueTask<T> PerformDeserializeAsync<T>(
            Stream source,
            CancellationToken cancellationToken)
        {
            object? instance = await MessagePackSerializer
                .Typeless
                .DeserializeAsync(
                    source,
                    options: Options,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return (T)instance;
        }
    }
}