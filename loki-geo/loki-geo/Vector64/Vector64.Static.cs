using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loki_geo
{
    public partial struct Vector64
    {
        public static Vector64 zero => (0, 0, 0);
        public static double Distance(Vector64 to, Vector64 from) => from.Distance(to);

        /// <summary>
        /// The angle, in radians, between two Vectors (assuming the pivot is (0,0,0))
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static double AngleBetween(Vector64 from, Vector64 to)
        {
            var dot = Dot(from, to);
            var prod_mags = from.magnitude * to.magnitude;

            return Math.Acos(dot / prod_mags);
        }

        public static double Dot(Vector64 a, Vector64 b)
        {
            var s1 = a.x * b.x;
            var s2 = a.y * b.y;
            var s3 = a.z * b.z;

            return s1 + s2 + s3;
        }

        public static Vector64 Cross(Vector64 a, Vector64 b)
        {
            var s1 = a.y * b.z - a.z * b.y;
            var s2 = a.z * b.x - a.x * b.z;
            var s3 = a.x * b.y - a.y * b.x;

            return new Vector64(s1, s2, s3);
        }

        public static Vector64 WeightedAverage(Vector64[] vectors, double[] weights)
        {
            if (weights.Length != vectors.Length)
            {
                double[] fixedWeights = new double[vectors.Length];
                for (int i = 0; i < vectors.Length; i++)
                {
                    try
                    {
                        fixedWeights[i] = weights[i];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        fixedWeights[i] = 1;
                    }
                }

                weights = fixedWeights;
            }

            Vector64 totalVec = Vector64.zero;
            double sum = 0;

            for (int i = 0; i < vectors.Length; i++)
            {
                totalVec += vectors[i] * weights[i];
                sum += weights[i];
            }

            return totalVec / sum;
        }

        /// <summary>
        /// Returns the maximum value along each axis.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector64 MaxComposite(Vector64 left, Vector64 right)
        {
            return (
                Math.Max(left.x, right.x),
                Math.Max(left.y, right.y),
                Math.Max(left.z, right.z)
            );
        }

        public static Vector64 MaxComposite(Vector64[] vectors)
        {
            Vector64 maxAll = vectors[0];

            for (int i = 1; i < vectors.Length; i++)
            {
                maxAll = MaxComposite(maxAll, vectors[i]);
            }

            return maxAll;
        }

        /// <summary>
        /// Returns the minimum value along each axis.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector64 MinComposite(Vector64 left, Vector64 right)
        {
            return (
                Math.Min(left.x, right.x),
                Math.Min(left.y, right.y),
                Math.Min(left.z, right.z)
            );
        }
        public static Vector64 MinComposite(Vector64[] vectors)
        {
            Vector64 minAll = vectors[0];

            for (int i = 1; i < vectors.Length; i++)
            {
                minAll = MinComposite(minAll, vectors[i]);
            }

            return minAll;
        }

        public static Vector64 Slerp(Vector64 from, Vector64 to, double t)
        {
            double angle = AngleBetween(from, to);
            var p0_numerator = Math.Sin((1 - t) * angle);
            var p1_numerator = Math.Sin(t * angle);

            var sine = Math.Sin(angle);

            var p0 = from * p0_numerator;
            var p1 = to * p1_numerator;

            return (p0 + p1) / sine;
        }
    }
}
