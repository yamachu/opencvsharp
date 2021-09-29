using OpenCvSharp.Internal.Vectors;
using System.Linq;
using Xunit;

namespace OpenCvSharp.Tests
{
    // ReSharper disable InconsistentNaming
    
    public class VectorTest : TestBase
    {
        [Fact]
        public void VectorOfMat()
        {
            var mats = new Mat[]
            {
                Mat.Eye(2, 2, MatType.CV_8UC1),
                Mat.Ones(2, 2, MatType.CV_64FC1),
            };

            using (var vec = new VectorOfMat(mats))
            {
                Assert.Equal(2, vec.Size);
                var dst = vec.ToArray();
                Assert.Equal(2, dst.Length);

                var eye = dst[0];
                var one = dst[1];

                Assert.Equal(1, eye.Get<byte>(0, 0));
                Assert.Equal(0, eye.Get<byte>(0, 1));
                Assert.Equal(0, eye.Get<byte>(1, 0));
                Assert.Equal(1, eye.Get<byte>(1, 1));

                Assert.Equal(1, one.Get<double>(0, 0), 6);
                Assert.Equal(1, one.Get<double>(0, 1), 6);
                Assert.Equal(1, one.Get<double>(1, 0), 6);
                Assert.Equal(1, one.Get<double>(1, 1), 6);

                foreach (var d in dst)
                {
                    d.Dispose();
                }
            }

            foreach (var mat in mats)
            {
                mat.Dispose();
            }
        }
        
        [Fact]
        public void VectorOfVectorMatCreate1()
        {
            using var vec = new VectorOfVectorMat();
        }

        [Fact]
        public void VectorOfVectorMatCreate2()
        {
            Mat[][] src = new[]
            {
                new[]{ 
                    new Mat(1, 1, MatType.CV_8UC1, Scalar.All(1)), 
                    new Mat(1, 1, MatType.CV_8UC1, Scalar.All(2)),
                    new Mat(1, 1, MatType.CV_8UC1, Scalar.All(3)), 
                },
                new[]{ 
                    new Mat(1, 1, MatType.CV_8UC1, Scalar.All(4)), 
                    new Mat(1, 1, MatType.CV_8UC1, Scalar.All(5)), 
                },
            };

            using var vec = new VectorOfVectorMat(src);

            {
                using var m00 = vec.GetAt(0, 0);
                Assert.Equal(1, m00.Get<byte>(0, 0));
                using var m01 = vec.GetAt(0, 1);
                Assert.Equal(2, m01.Get<byte>(0, 0));
                using var m02 = vec.GetAt(0, 2);
                Assert.Equal(3, m02.Get<byte>(0, 0));
                using var m10 = vec.GetAt(1, 0);
                Assert.Equal(4, m10.Get<byte>(0, 0));
                using var m11 = vec.GetAt(1, 1);
                Assert.Equal(5, m11.Get<byte>(0, 0));
            }

            var array = vec.ToArray();
            try
            {
                Assert.Equal(2, array.Length);
                Assert.Equal(3, array[0].Length);
                Assert.Equal(2, array[1].Length);
                
                using var m00 = array[0][0];
                Assert.Equal(1, m00.Get<byte>(0, 0));
                using var m01 = array[0][1];
                Assert.Equal(2, m01.Get<byte>(0, 0));
                using var m02 = array[0][2];
                Assert.Equal(3, m02.Get<byte>(0, 0));
                using var m10 = array[1][0];
                Assert.Equal(4, m10.Get<byte>(0, 0));
                using var m11 = array[1][1];
                Assert.Equal(5, m11.Get<byte>(0, 0));
            }
            finally
            {
                foreach (var m in array.SelectMany(mm => mm))
                {
                    m.Dispose();
                }
            }
        }
    }
}