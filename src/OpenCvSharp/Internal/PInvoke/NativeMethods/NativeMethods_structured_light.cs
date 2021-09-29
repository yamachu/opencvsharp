using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

#pragma warning disable 1591
#pragma warning disable CA1401 // P/Invokes should not be visible
#pragma warning disable IDE1006 // Naming style
// ReSharper disable InconsistentNaming

namespace OpenCvSharp.Internal
{
    static partial class NativeMethods
    {
        // BriefDescriptorExtractor

        [Pure, DllImport(DllExtern, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ExceptionStatus structured_light_StructuredLightPattern_generate(
            IntPtr obj,
            IntPtr patternImages,
            out int returnValue);

        [Pure, DllImport(DllExtern, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ExceptionStatus structured_light_StructuredLightPattern_decode(
           IntPtr obj,
           IntPtr patternImages,
           IntPtr disparityMap,
           IntPtr? blackImages,
           IntPtr? whiteImages,
           int flags,
           out int returnValue);
    }
}