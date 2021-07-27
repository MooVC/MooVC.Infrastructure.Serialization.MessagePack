namespace MooVC.Infrastructure.Serialization.MessagePack.TypedSerializerTests
{
    using global::MessagePack;
    using Xunit;

    public sealed class WhenSerializerIsConstructed
    {
        [Fact]
        public void GivenNoOptionsThenADefaultSerializerIsCreated()
        {
            var serializer = new TypedSerializer();

            AssertEqual(serializer);
        }

        [Fact]
        public void GivenOptionsThenASerializerIsCreatedWithTheOptionsApplied()
        {
            MessagePackSerializerOptions options = MessagePackSerializerOptions
                .Standard
                .WithCompression(MessagePackCompression.Lz4BlockArray)
                .WithOmitAssemblyVersion(true);

            var serializer = new TypedSerializer(options: options);

            AssertEqual(serializer, options: options);
        }

        private static void AssertEqual(
            Serializer serializer,
            MessagePackSerializerOptions? options = default)
        {
            options ??= MessagePackSerializerOptions.Standard;

            Assert.Equal(options.AllowAssemblyVersionMismatch, serializer.Options.AllowAssemblyVersionMismatch);
            Assert.Equal(options.Compression, serializer.Options.Compression);
            Assert.Equal(options.OldSpec, serializer.Options.OldSpec);
            Assert.Equal(options.OmitAssemblyVersion, serializer.Options.OmitAssemblyVersion);
            Assert.Equal(options.Resolver, serializer.Options.Resolver);
            Assert.Equal(options.Security, serializer.Options.Security);
        }
    }
}