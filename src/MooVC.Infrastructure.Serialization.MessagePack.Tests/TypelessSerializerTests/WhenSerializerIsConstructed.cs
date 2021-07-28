namespace MooVC.Infrastructure.Serialization.MessagePack.TypelessSerializerTests
{
    using global::MessagePack;
    using Xunit;

    public sealed class WhenSerializerIsConstructed
    {
        [Fact]
        public void GivenNoOptionsConfigurationThenADefaultSerializerIsCreated()
        {
            var serializer = new TypelessSerializer();

            AssertEqual(serializer);
        }

        [Fact]
        public void GivenOptionsConfigurationThenASerializerIsCreatedWithTheOptionsApplied()
        {
            MessagePackSerializerOptions options = MessagePackSerializerOptions
                .Standard
                .WithCompression(MessagePackCompression.Lz4BlockArray)
                .WithOmitAssemblyVersion(true);

            var serializer = new TypelessSerializer(configure: _ => options);

            AssertEqual(serializer, options: options);
        }

        private static void AssertEqual(
            Serializer serializer,
            MessagePackSerializerOptions? options = default)
        {
            options ??= MessagePackSerializer.Typeless.DefaultOptions;

            Assert.Equal(options.AllowAssemblyVersionMismatch, serializer.Options.AllowAssemblyVersionMismatch);
            Assert.Equal(options.Compression, serializer.Options.Compression);
            Assert.Equal(options.OldSpec, serializer.Options.OldSpec);
            Assert.Equal(options.OmitAssemblyVersion, serializer.Options.OmitAssemblyVersion);
            Assert.Equal(options.Resolver, serializer.Options.Resolver);
            Assert.Equal(options.Security, serializer.Options.Security);
        }
    }
}