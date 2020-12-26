// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/native-tools-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace lithiumtoast.NativeTools
{
    /// <summary>
    ///     Defines the runtime platforms (operating system + computer architecture).
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    public enum RuntimePlatform
    {
        /// <summary>
        ///     Unknown target platform.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Desktop versions of Windows on 32-bit or 64-bit computing architecture.
        /// </summary>
        Windows = 1,

        /// <summary>
        ///     Desktop versions of macOS on 64-bit computing architecture.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
        macOS = 2,

        /// <summary>
        ///     Desktop distributions of the Linux operating system on 64-bit computing architecture.
        /// </summary>
        Linux = 3,

        /// <summary>
        ///     Mobile versions of Android on 64-bit computing architecture.
        /// </summary>
        Android = 4,

        /// <summary>
        ///     Mobile versions of iOS on 64-bit computing architecture.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Product name.")]
        [SuppressMessage("ReSharper", "SA1300", Justification = "Product name.")]
        iOS = 5,

        // TODO: tvOS, RaspberryPi, WebAssembly, PlayStation4, PlayStationVita, Switch etc
    }
}
