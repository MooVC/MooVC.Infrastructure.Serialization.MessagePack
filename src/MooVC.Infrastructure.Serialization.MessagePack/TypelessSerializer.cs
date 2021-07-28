namespace MooVC.Infrastructure.Serialization.MessagePack
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using global::MessagePack;

    public sealed class TypelessSerializer
        : Serializer
    {
        public TypelessSerializer(Func<MessagePackSerializerOptions, MessagePackSerializerOptions>? configure = default)
            : base(configure, MessagePackSerializer.Typeless.DefaultOptions)
        {
        }

        public override Task SerializeAsync<T>(
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