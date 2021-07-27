namespace MooVC.Infrastructure.Serialization.MessagePack
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using global::MessagePack;

    public sealed class TypedSerializer
        : Serializer
    {
        public TypedSerializer(MessagePackSerializerOptions? options = default)
            : base(options ?? MessagePackSerializer.DefaultOptions)
        {
        }

        public override Task SerializeAsync<T>(
            T instance,
            Stream target,
            CancellationToken? cancellationToken = default)
        {
            return MessagePackSerializer.SerializeAsync(
                target,
                instance,
                options: Options,
                cancellationToken: cancellationToken.GetValueOrDefault());
        }

        protected override ValueTask<T> PerformDeserializeAsync<T>(
            Stream source,
            CancellationToken cancellationToken)
        {
            return MessagePackSerializer
                .DeserializeAsync<T>(
                    source,
                    options: Options,
                    cancellationToken: cancellationToken);
        }
    }
}