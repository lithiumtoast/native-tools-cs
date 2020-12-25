// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace NativeTools
{
    /// <summary>
    ///     Defines the runtime platforms.
    /// </summary>
    [Flags]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public enum RuntimePlatform
    {
        /// <summary>
        ///     Unknown target platform.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Desktop versions of Windows on 64-bit computing architecture.
        /// </summary>
        Windows = 1 << 0,

        /// <summary>
        ///     Desktop versions of macOS on 64-bit computing architecture.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
        macOS = 1 << 1,

        /// <summary>
        ///     Desktop distributions of the Linux operating system on 64-bit computing architecture.
        /// </summary>
        Linux = 1 << 2,

        /// <summary>
        ///     Mobile versions of Android on 64-bit computing architecture.
        /// </summary>
        Android = 1 << 3,

        /// <summary>
        ///     Mobile versions of iOS on 64-bit computing architecture.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
        iOS = 1 << 4,

        // TODO: tvOS, RaspberryPi, WebAssembly, PlayStation4, PlayStationVita, Switch etc
    }
}
