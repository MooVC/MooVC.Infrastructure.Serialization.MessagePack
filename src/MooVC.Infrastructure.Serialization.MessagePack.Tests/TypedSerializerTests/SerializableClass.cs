namespace MooVC.Infrastructure.Serialization.MessagePack.TypedSerializerTests;

using System.Collections.Generic;
using global::MessagePack;

[MessagePackObject(keyAsPropertyName: true)]
public sealed class SerializableClass
    : ISerializableInstance
{
    public IEnumerable<ulong>? Array { get; init; }

    public int? Integer { get; init; }

    public ISerializableInstance? Object { get; init; }

    public string? String { get; init; }
}