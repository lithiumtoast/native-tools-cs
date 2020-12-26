// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/native-tools-cs) for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NativeTools
{
    public static unsafe class UnmanagedStrings
    {
        private static readonly Dictionary<string, IntPtr> StringsToPointers = new();
        private static readonly Dictionary<IntPtr, string> PointersToStrings = new();

        public static string GetString(in byte* pointer)
        {
            return PointersToStrings.TryGetValue((IntPtr)pointer, out var value) ? value : string.Empty;
        }

        public static byte* GetBytes(string @string)
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

        public static void Clear()
        {
            foreach (var pointer in PointersToStrings.Keys)
            {
                Marshal.FreeHGlobal(pointer);
            }

            PointersToStrings.Clear();
            StringsToPointers.Clear();

            GC.Collect();
        }

        public static void Clear(IntPtr pointer)
        {
            if (!PointersToStrings.ContainsKey(pointer))
            {
                return;
            }

            var str = PointersToStrings[pointer];
            PointersToStrings.Remove(pointer);
            StringsToPointers.Remove(str);
            Marshal.FreeHGlobal(pointer);

            GC.Collect();
        }
    }
}
