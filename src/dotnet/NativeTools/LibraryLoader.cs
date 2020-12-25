// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NativeTools
{
    public static class LibraryLoader
    {
        private static IEnumerable<string>? _librarySearchDirectories;

        public static void SetDllImportResolver(Assembly assembly)
        {
            NativeLibrary.SetDllImportResolver(assembly, Resolver);
        }

        [SuppressMessage("ReSharper", "CommentTypo", Justification = "Flags.")]
        public static IntPtr LoadLibrary(string libraryFilePath)
        {
            return NativeLibrary.Load(libraryFilePath);
        }

        public static void FreeLibrary(IntPtr libraryHandle)
        {
            NativeLibrary.Free(libraryHandle);
        }

        public static IntPtr GetLibraryFunctionPointer(IntPtr libraryHandle, string functionName)
        {
            NativeLibrary.TryGetExport(libraryHandle, functionName, out var functionPointer);
            return functionPointer;
        }

        public static T GetLibraryFunction<T>(IntPtr libraryHandle)
        {
            return GetLibraryFunction<T>(libraryHandle, string.Empty);
        }

        public static T GetLibraryFunction<T>(IntPtr libraryHandle, string functionName)
        {
            if (string.IsNullOrEmpty(functionName))
            {
                functionName = typeof(T).Name;
                if (functionName.ToLower().StartsWith("d_", StringComparison.Ordinal))
                {
                    functionName = functionName.Substring(2);
                }
            }

            var functionHandle = GetLibraryFunctionPointer(libraryHandle, functionName);
            if (functionHandle == IntPtr.Zero)
            {
                throw new Exception($"Could not find a function with the given name '{functionName}' in the library.");
            }

            return Marshal.GetDelegateForFunctionPointer<T>(functionHandle);
        }

        private static string GetLibraryFileExtension(RuntimePlatform platform)
        {
            return platform switch
            {
                RuntimePlatform.Windows => ".dll",
                RuntimePlatform.macOS => ".dylib",
                RuntimePlatform.Linux => ".so",
                RuntimePlatform.Android => throw new NotImplementedException(),
                RuntimePlatform.iOS => throw new NotImplementedException(),
                RuntimePlatform.Unknown => throw new NotSupportedException(),
                _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, null)
            };
        }

        private static string GetRuntimeIdentifier(RuntimePlatform platform)
        {
            return platform switch
            {
                RuntimePlatform.Windows => Environment.Is64BitProcess ? "win-x64" : "win-x86",
                RuntimePlatform.macOS => "osx-x64",
                RuntimePlatform.Linux => "linux-x64",
                RuntimePlatform.Android => throw new NotImplementedException(),
                RuntimePlatform.iOS => throw new NotImplementedException(),
                RuntimePlatform.Unknown => throw new NotSupportedException(),
                _ => throw new ArgumentOutOfRangeException(nameof(RuntimePlatform), platform, null)
            };
        }

        private static IEnumerable<string> GetSearchDirectories(RuntimePlatform platform)
        {
            if (_librarySearchDirectories != null)
            {
                return _librarySearchDirectories;
            }

            var runtimeIdentifier = GetRuntimeIdentifier(platform);

            var librarySearchDirectories = new List<string>
            {
                Environment.CurrentDirectory,
                AppDomain.CurrentDomain.BaseDirectory,
                $"libs/{runtimeIdentifier}",
                $"runtimes/{runtimeIdentifier}/native"
            };

            return _librarySearchDirectories = librarySearchDirectories.ToArray();
        }

        private static bool TryGetLibraryPath(RuntimePlatform platform, string libraryName, out string libraryFilePath)
        {
            var libraryPrefix = platform == RuntimePlatform.Windows ? string.Empty : "lib";
            var libraryFileExtension = GetLibraryFileExtension(platform);
            var libraryFileName = $"{libraryPrefix}{libraryName}";

            var directories = GetSearchDirectories(platform);
            foreach (var directory in directories)
            {
                if (TryFindLibraryPath(directory, libraryFileExtension, libraryFileName, out libraryFilePath))
                {
                    return true;
                }
            }

            libraryFilePath = string.Empty;
            return false;
        }

        private static bool TryFindLibraryPath(
            string directoryPath,
            string libraryFileExtension,
            string libraryFileNameWithoutExtension,
            out string result)
        {
            if (!Directory.Exists(directoryPath))
            {
                result = string.Empty;
                return false;
            }

            var searchPattern = $"*{libraryFileExtension}";
            var filePaths = Directory.EnumerateFiles(directoryPath, searchPattern);
            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (!fileName.StartsWith(libraryFileNameWithoutExtension, StringComparison.Ordinal))
                {
                    continue;
                }

                result = filePath;
                return true;
            }

            result = string.Empty;
            return false;
        }

        private static IntPtr Resolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            IntPtr libraryHandle;

            var platform = Runtime.Platform;
            if (TryGetLibraryPath(platform, libraryName, out var libraryFilePath))
            {
                libraryHandle = LoadLibrary(libraryFilePath);
                return libraryHandle;
            }

            if (NativeLibrary.TryLoad(libraryName, assembly, searchPath, out libraryHandle))
            {
                return libraryHandle;
            }

            throw new Exception($"Could not find the native library: {libraryName}. Did you forget to place a native library in the correct file path?");
        }
    }
}
