﻿namespace MooVC.Infrastructure.Serialization.MessagePack.TypelessSerializerTests;

using System.Collections.Generic;

internal sealed class SerializableClass
    : ISerializableInstance
{
    public IEnumerable<ulong>? Array { get; init; }

    public int? Integer { get; init; }

    public ISerializableInstance? Object { get; init; }

    public string? String { get; init; }
}