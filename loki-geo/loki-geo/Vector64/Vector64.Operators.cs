using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loki_geo
{
    public partial struct Vector64
    {
        public static Vector64 operator +(Vector64 left, Vector64 right)
        {
            return new Vector64(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        public static Vector64 operator -(Vector64 left, Vector64 right)
        {
            return new Vector64(left.x - right.x, left.y - right.y, left.z - right.z);
        }

        public static Vector64 operator *(Vector64 left, double right)
        {
            return new Vector64(left.x * right, left.y * right, left.z * right);
        }
        public static Vector64 operator *(double left, Vector64 right)
        {
            return new Vector64(right.x * left, right.y * left, right.z * left);
        }

        public static Vector64 operator /(Vector64 left, double right)
        {
            return left * (1 / right);
        }

        // Negative sign operator, duh?
        public static Vector64 operator -(Vector64 vec)
        {
            return new Vector64(-vec.x, -vec.y, -vec.z);
        }

        /// <summary>
        /// Implicit cast from a tuple of doubles to make code look clean?
        /// </summary>
        /// <param name="tuple"></param>
        public static implicit operator Vector64((double x, double y, double z) tuple)
        {
            return new Vector64(tuple.x, tuple.y, tuple.z);
        }
    }
}
