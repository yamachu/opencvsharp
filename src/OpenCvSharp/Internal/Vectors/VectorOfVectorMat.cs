using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp.Internal.Util;

namespace OpenCvSharp.Internal.Vectors
{
    /// <summary> 
    /// </summary>
    public class VectorOfVectorMat : DisposableCvObject, IStdVector<Mat[]>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VectorOfVectorMat()
        {
            ptr = NativeMethods.vector_vector_Mat_new1();
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public VectorOfVectorMat(IEnumerable<IEnumerable<Mat>> mats)
        {
            if (mats is null)
                throw new ArgumentNullException(nameof(mats));

            var matsArray = mats.Select(mm => mm.ToArray()).ToArray();
            var flattenMatsPtrs = matsArray.SelectMany(mm => mm).Select(m => m.CvPtr).ToArray();
            var size1 = (nuint)matsArray.Length;
            var size2List = matsArray.Select(mm => (nuint)mm.Length).ToArray();

            ptr = NativeMethods.vector_vector_Mat_new2(flattenMatsPtrs, size1, size2List);

            GC.KeepAlive(mats);
        }

        /// <summary>
        /// Releases unmanaged resources
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            NativeMethods.vector_vector_Mat_delete(ptr);
            base.DisposeUnmanaged();
        }

        /// <summary>
        /// vector.size()
        /// </summary>
        public int GetSize1()
        {
            var res = NativeMethods.vector_vector_Mat_getSize1(ptr);
            GC.KeepAlive(this);
            return (int)res;
        }

        /// <summary>
        /// vector.size()
        /// </summary>
        public int Size => GetSize1();

        /// <summary>
        /// vector[i].size()
        /// </summary>
        public IReadOnlyList<long> GetSize2()
        {
            var size1 = GetSize1();
            var size2 = new nuint[size1];
            NativeMethods.vector_vector_Mat_getSize2(ptr, size2);
            GC.KeepAlive(this);
            return size2.Select(s => (long)s).ToArray();
        }
        
        /// <summary>
        /// Indexer
        /// </summary>
        /// <returns></returns>
        public Mat GetAt(int i, int j)
        {            
            var p = NativeMethods.vector_vector_Mat_getAt(ptr, (nuint)i, (nuint)j);
            GC.KeepAlive(this);
            return new Mat(p){IsEnabledDispose=false};
        }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public Mat this[int i, int j]
        {
            get => GetAt(i, j);
        }

        /// <summary>
        /// Converts std::vector to managed array
        /// </summary>
        /// <returns></returns>
        public Mat[][] ToArray()
        {
            return ToArray<Mat>();
        }

        /// <summary>
        /// Converts std::vector to managed array
        /// </summary>
        /// <returns></returns>
        public T[][] ToArray<T>()
            where T : Mat, new()
        {
            var size1 = GetSize1();
            if (size1 == 0)
                return Array.Empty<T[]>();
            var size2Array = GetSize2();

            var dst2d = new T[size1][];
            for (var i = 0; i < size1; i++)
            {
                dst2d[i] = ToArray1D<T>(i, size2Array[i]);
            }

            GC.KeepAlive(this);

            return dst2d;
        }

        /// <summary>
        /// Converts std::vector to managed array
        /// </summary>
        /// <returns></returns>
        private T[] ToArray1D<T>(int i, long size)
            where T : Mat, new()
        {
            if (size == 0)
                return Array.Empty<T>();

            var dst = new T[size];
            var dstPtr = new IntPtr[size];
            for (var j = 0; j < size; j++)
            {
                var m = new T();
                dst[j] = m;
                dstPtr[j] = m.CvPtr;
            }

            NativeMethods.vector_vector_Mat_assignToArray(ptr, i, dstPtr);
            return dst;
        }
    }
}
