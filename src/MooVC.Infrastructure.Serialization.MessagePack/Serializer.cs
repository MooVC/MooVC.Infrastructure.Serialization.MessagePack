namespace MooVC.Infrastructure.Serialization.MessagePack
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using global::MessagePack;
    using MooVC.Compression;
    using static System.String;
    using static MooVC.Infrastructure.Serialization.MessagePack.Resources;
    using Base = MooVC.Serialization.Serializer;

    public abstract class Serializer
        : Base
    {
        protected Serializer(
            ICompressor? compressor = default,
            Func<MessagePackSerializerOptions, MessagePackSerializerOptions>? configure = default,
            MessagePackSerializerOptions? options = default)
            : base(compressor: compressor)
        {
            if (configure is { })
            {
                options = configure.Invoke(options ?? Options);
            }

            if (options is { })
            {
                Options = options;
            }
        }

        public MessagePackSerializerOptions Options { get; } = MessagePackSerializerOptions.Standard;

        protected override async Task<T> PerformDeserializeAsync<T>(
            Stream source,
            CancellationToken? cancellationToken = default)
        {
            T? result = await
                PerformDeserializeAsync<T>(
                    source,
                    cancellationToken.GetValueOrDefault())
                .ConfigureAwait(false);

            if (result is null)
            {
                throw new MessagePackSerializationException(Format(
                    SerializerPerformDeserializeAsyncFailure,
                    typeof(T).AssemblyQualifiedName));
            }

            return result;
        }

        protected abstract ValueTask<T> PerformDeserializeAsync<T>(
            Stream source,
            CancellationToken cancellationToken)
            where T : notnull;
    }
}