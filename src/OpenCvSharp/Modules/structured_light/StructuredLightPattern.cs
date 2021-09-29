using System;
using System.Collections.Generic;
using OpenCvSharp.Internal;
using OpenCvSharp.Internal.Vectors;

// ReSharper disable once InconsistentNaming

namespace OpenCvSharp.StructuredLight
{
    /// <summary>
    /// Abstract base class for generating and decoding structured light patterns.
    /// </summary>
    public abstract class StructuredLightPattern : Algorithm
    {
        /// <summary>
        /// Kyriakos Herakleous, Charalambos Poullis. 
        /// "3DUNDERWORLD-SLS: An Open-Source Structured-Light Scanning System for Rapid Geometry Acquisition", 
        /// arXiv preprint arXiv:1406.6595 (2014).
        /// </summary>
        public const int DECODE_3D_UNDERWORLD = 0;

        /// <summary>
        /// Generates the structured light pattern to project.
        /// </summary>
        /// <param name="patternImages">The generated pattern: a vector&lt;Mat&gt;, in which each image is a CV_8U Mat at projector's resolution.</param>
        /// <returns></returns>
        public virtual bool Generate(OutputArray patternImages)
        {
            if (patternImages is null)
                throw new ArgumentNullException(nameof(patternImages));

            NativeMethods.HandleException(
                NativeMethods.structured_light_StructuredLightPattern_generate(
                    ptr, patternImages.CvPtr, out var ret));
            GC.KeepAlive(this);

            return ret != 0;
        }

        /// <summary>
        /// Decodes the structured light pattern, generating a disparity map
        /// </summary>
        /// <param name="patternImages">The acquired pattern images to decode (vector&lt;vector&lt;Mat&gt;&gt;), loaded as grayscale and previously rectified.</param>
        /// <param name="disparityMap">The decoding result: a CV_64F Mat at image resolution, storing the computed disparity map.</param>
        /// <param name="blackImages">The all-black images needed for shadowMasks computation.</param>
        /// <param name="whiteImages">The all-white images needed for shadowMasks computation.</param>
        /// <param name="flags">Flags setting decoding algorithms. Default: DECODE_3D_UNDERWORLD.</param>
        /// <remarks>All the images must be at the same resolution.</remarks>
        /// <returns></returns>
        public virtual bool Decode(
            IEnumerable<IEnumerable<Mat>> patternImages,
            OutputArray disparityMap,
            InputArray? blackImages = null,
            InputArray? whiteImages = null,
            int flags = DECODE_3D_UNDERWORLD)
        {
            if (patternImages is null)
                throw new ArgumentNullException(nameof(patternImages));
            if (patternImages is null)
                throw new ArgumentNullException(nameof(disparityMap));
            
            using var vec = new VectorOfVectorMat(patternImages);
            
            NativeMethods.HandleException(
                NativeMethods.structured_light_StructuredLightPattern_decode(
                    ptr, 
                    vec.CvPtr,
                    disparityMap.CvPtr,
                    blackImages?.CvPtr ?? IntPtr.Zero, 
                    whiteImages?.CvPtr ?? IntPtr.Zero,
                    flags,
                    out var ret));
            
            GC.KeepAlive(this);
            GC.KeepAlive(patternImages);

            return ret != 0;
        }
    }
}
