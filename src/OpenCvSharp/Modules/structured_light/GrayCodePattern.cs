using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp.Internal;
using OpenCvSharp.Internal.Util;
using OpenCvSharp.Internal.Vectors;

// ReSharper disable once InconsistentNaming

namespace OpenCvSharp.StructuredLight
{

    /// <summary>
    /// FREAK implementation
    /// </summary>
    public class GrayCodePattern : StructuredLightPattern
    {
        private Ptr? ptrObj;

        /// <summary>
        /// Constructor
        /// </summary>
        protected GrayCodePattern(IntPtr p)
        {
            ptrObj = new Ptr(p);
            ptr = ptrObj.Get();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">Projector's width. Default value is 1024.</param>
        /// <param name="height">Projector's height. Default value is 768.</param>
        public static GrayCodePattern Create(
            int width = 1024, int height = 768)
        {
            throw new NotImplementedException();
            //return new GrayCodePattern(ret);
        }

        /// <summary>
        /// Releases managed resources
        /// </summary>
        protected override void DisposeManaged()
        {
            ptrObj?.Dispose();
            ptrObj = null;
            base.DisposeManaged();
        }
        
        internal class Ptr : OpenCvSharp.Ptr
        {
            public Ptr(IntPtr ptr) : base(ptr)
            {
            }

            public override IntPtr Get()
            {
                throw new NotImplementedException();
                //NativeMethods.HandleException(
                //    NativeMethods.xfeatures2d_Ptr_FREAK_get(ptr, out var ret));
                GC.KeepAlive(this);
                //return ret;
            }

            protected override void DisposeUnmanaged()
            {
                //NativeMethods.HandleException(
                //    NativeMethods.xfeatures2d_Ptr_FREAK_delete(ptr));
                base.DisposeUnmanaged();
            }
        }
    }
}
