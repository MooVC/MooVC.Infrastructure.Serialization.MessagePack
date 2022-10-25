namespace MooVC.Infrastructure.Serialization.MessagePack.TypelessSerializerTests;

using System.Collections.Generic;

internal sealed record SerializableRecord(IEnumerable<ulong>? Array, int? Integer, ISerializableInstance? Object, string? String)
    : ISerializableInstance;