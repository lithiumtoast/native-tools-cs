// <auto-generated/>
// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/native-tools-cs) for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

#nullable enable

namespace lithiumtoast.NativeTools
{
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public static unsafe partial class Native
    {
        private static readonly Dictionary<string, IntPtr> StringsToPointers = new Dictionary<string, IntPtr>();
        private static readonly Dictionary<IntPtr, string> PointersToStrings = new Dictionary<IntPtr, string>();

        /// <summary>
        ///     Gets a <see cref="string" /> from a C style string (one dimensional <see cref="byte" /> array terminated by a
        ///     <c>0x0</c>).
        /// </summary>
        /// <param name="pointer">A pointer to the C string.</param>
        /// <returns>A <see cref="string" /> equivalent to the C string pointed by <paramref name="pointer" />.</returns>
        public static string GetStringFromBytePointer([In] byte* pointer)
        {
            if (PointersToStrings.TryGetValue((IntPtr)pointer, out var @string))
            {
                return @string;
            }

            @string = Marshal.PtrToStringAnsi((IntPtr)pointer);
            if (string.IsNullOrEmpty(@string))
            {
                return string.Empty;
            }

            PointersToStrings.Add((IntPtr)pointer, @string);
            StringsToPointers.Add(@string, (IntPtr)pointer);

            return @string;
        }

        /// <summary>
        ///     Gets a <see cref="string" /> from a C style string (one dimensional <see cref="byte" /> array terminated by a
        ///     <c>0x0</c>).
        /// </summary>
        /// <param name="pointer">A pointer to the C string.</param>
        /// <returns>A <see cref="string" /> equivalent to the C string pointed by <paramref name="pointer" />.</returns>
        public static string GetStringFromIntPtr(IntPtr pointer)
        {
            return GetStringFromBytePointer((byte*)pointer);
        }

        /// <summary>
        ///     Gets a pointer to a C string (a one dimensional <see cref="byte" /> array terminated by a <c>0x0</c>) from a
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="string">A <see cref="string" />.</param>
        /// <returns>A pointer to a C string (a one dimensional <see cref="byte" /> array terminated by a <c>0x0</c>).</returns>
        public static byte* GetBytePointerFromString(string @string)
        {
            if (StringsToPointers.TryGetValue(@string, out var pointer))
            {
                return (byte*)pointer;
            }

            pointer = Marshal.StringToHGlobalAnsi(@string);
            PointersToStrings.Add(pointer, @string);
            StringsToPointers.Add(@string, pointer);

            return (byte*)pointer;
        }

        /// <summary>
        ///     Gets a pointer to a C string (a one dimensional <see cref="byte" /> array terminated by a <c>0x0</c>) from a
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="string">A <see cref="string" />.</param>
        /// <returns>A pointer to a C string (a one dimensional <see cref="byte" /> array terminated by a <c>0x0</c>).</returns>
        public static IntPtr GetIntPtrFromString(string @string)
        {
            return (IntPtr)GetBytePointerFromString(@string);
        }

        /// <summary>
        ///     Frees all <see cref="string" /> objects allocated by <see cref="GetStringFromBytePointer" />,
        ///     <see cref="GetStringFromIntPtr" />, <see cref="GetBytePointerFromString" />, or <see cref="GetIntPtrFromString" />.
        /// </summary>
        public static void ClearStrings()
        {
            foreach (var pointer in PointersToStrings.Keys)
            {
                Marshal.FreeHGlobal(pointer);
            }

            PointersToStrings.Clear();
            StringsToPointers.Clear();
        }
        
        /// <summary>
        ///     Frees the <see cref="string" /> object allocated by <see cref="GetBytePointerFromString" /> or <see cref="GetIntPtrFromString" />.
        /// </summary>
        /// <param name="pointer">A pointer to the C string.</param>
        public static void ClearString(IntPtr pointer)
        {
            if (PointersToStrings.ContainsKey(pointer))
            {
                PointersToStrings.Remove(pointer);
            }

            string? @string = null;
            foreach (var (key, value) in StringsToPointers)
            {
                if (value == pointer)
                {
                    @string = key;
                    break;
                }
            }

            if (@string != null)
            {
                StringsToPointers.Remove(@string);
            }

            Marshal.FreeHGlobal(pointer);
        }

        /// <summary>
        ///     Frees the <see cref="string" /> object allocated by <see cref="GetBytePointerFromString" /> or <see cref="GetIntPtrFromString" />.
        /// </summary>
        /// <param name="pointer">A pointer to the C string.</param>
        public static void ClearString([In] byte* pointer)
        {
            ClearString((IntPtr)pointer);
        }
    }
}