// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/native-tools-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///     A boolean value type with the same memory layout as a <see cref="byte" /> in both managed and unmanaged
///     contexts; equivalent to a standard bool (_Bool) found in C/C++/ObjC where <c>0</c> is <c>false</c> and <c>1</c> is
///     <c>true</c>.
/// </summary>
/// <remarks>
///     <para>
///         In the world of .NET, data is often represented in memory differently depending on the context of being
///         managed (.NET) and unmanaged (C/C++/ObjC). Blittable types are data types which have the same bit
///         representation in both managed and unmanaged contexts. The <see cref="bool" /> found in .NET is not
///         blittable (!). If data types have the same bit representation between contexts they can be "blitted"
///         (block bit transfer) between the contexts with minimal to zero overhead. Understanding the difference
///         between blittable and non-blittable types help in having correct and highly performant interoperability between
///         managed (.NET) applications/libraries and unmanaged (C/C++/ObjC) applications/libraries.
///     </para>
/// </remarks>
[SuppressMessage("ReSharper", "CheckNamespace", Justification = "BlittableBoolean wants to be a builtin type.")]
[SuppressMessage("ReSharper", "CA1050", Justification = "BlittableBoolean wants to be a builtin type.")]
public readonly struct BlittableBoolean
{
    private readonly byte _value;

    private BlittableBoolean(bool value)
    {
        _value = Convert.ToByte(value);
    }

    /// <summary>
    ///     Converts the specified <see cref="bool" /> to a <see cref="BlittableBoolean" />.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="BlittableBoolean" />.</returns>
    public static implicit operator BlittableBoolean(bool value)
    {
        return new(value);
    }

    /// <summary>
    ///     Converts the specified <see cref="BlittableBoolean" /> to a <see cref="bool" />.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="bool" />.</returns>
    public static implicit operator bool(BlittableBoolean value)
    {
        return Convert.ToBoolean(value._value);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Convert.ToBoolean(_value).ToString();
    }
}
