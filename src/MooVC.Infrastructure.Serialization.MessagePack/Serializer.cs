namespace MooVC.Infrastructure.Serialization.MessagePack
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::MessagePack;
    using MooVC.Serialization;
    using static System.String;
    using static MooVC.Infrastructure.Serialization.MessagePack.Resources;

    public abstract class Serializer
        : ISerializer
    {
        protected Serializer(
            Func<MessagePackSerializerOptions, MessagePackSerializerOptions>? configure,
            MessagePackSerializerOptions? options)
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

        public async Task<T> DeserializeAsync<T>(
            IEnumerable<byte> data,
            CancellationToken? cancellationToken = default)
            where T : notnull
        {
            using var stream = new MemoryStream(data.ToArray());

            return await
                DeserializeAsync<T>(
                    stream,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<T> DeserializeAsync<T>(
            Stream source,
            CancellationToken? cancellationToken = default)
            where T : notnull
        {
            T? result = await
                PerformDeserializeAsync<T>(
                    source,
                    cancellationToken.GetValueOrDefault())
                .ConfigureAwait(false);

            if (result is null)
            {
                throw new MessagePackSerializationException(Format(
                    SerializerDeserializeFailure,
                    typeof(T).AssemblyQualifiedName));
            }

            return result;
        }

        public async Task<IEnumerable<byte>> SerializeAsync<T>(
            T instance,
            CancellationToken? cancellationToken = default)
            where T : notnull
        {
            using var stream = new MemoryStream();

            await
                SerializeAsync(
                    instance,
                    stream,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return stream.ToArray();
        }

        public abstract Task SerializeAsync<T>(
            T instance,
            Stream target,
            CancellationToken? cancellationToken = default)
            where T : notnull;

        protected abstract ValueTask<T> PerformDeserializeAsync<T>(
            Stream source,
            CancellationToken cancellationToken)
            where T : notnull;
    }
}