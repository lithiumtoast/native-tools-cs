// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System.Runtime.InteropServices;

namespace NativeTools
{
    public static class Runtime
    {
        public static RuntimePlatform Platform { get; } = GetPlatform();

        private static RuntimePlatform GetPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return RuntimePlatform.Windows;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return RuntimePlatform.macOS;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return RuntimePlatform.Linux;
            }

            return RuntimePlatform.Unknown;
        }
    }
}
