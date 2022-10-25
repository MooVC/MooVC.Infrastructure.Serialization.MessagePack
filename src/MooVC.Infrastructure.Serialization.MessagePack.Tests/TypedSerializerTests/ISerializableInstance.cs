namespace MooVC.Infrastructure.Serialization.MessagePack.TypedSerializerTests;

using System.Collections.Generic;
using global::MessagePack;

[Union(0, typeof(SerializableClass))]
[Union(1, typeof(SerializableRecord))]
public interface ISerializableInstance
{
    IEnumerable<ulong>? Array { get; }

    int? Integer { get; }

    ISerializableInstance? Object { get; }

    string? String { get; }
}