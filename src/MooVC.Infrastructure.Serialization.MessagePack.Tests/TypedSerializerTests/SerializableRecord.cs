namespace MooVC.Infrastructure.Serialization.MessagePack.TypedSerializerTests;

using System.Collections.Generic;
using global::MessagePack;

[MessagePackObject(keyAsPropertyName: true)]
public sealed record SerializableRecord(IEnumerable<ulong>? Array, int? Integer, ISerializableInstance? Object, string? String)
    : ISerializableInstance;