using System;

namespace loki_geo
{
    /// <summary>
    /// A 64-bit cartesian coordinate (X,Y,Z) struct.
    /// </summary>
    [System.Serializable]
    public partial struct Vector64
    {
        public double x, y, z;

        public Vector64(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public double magnitude => zero.Distance(this);

        public Vector64 normalized =>
            SquareMagnitude == 0 ? this : this / magnitude;

        public double Distance(Vector64 to)
        {
            double sqrMag = (to - this).SquareMagnitude;
            return Math.Sqrt(sqrMag);
        }

        public double SquareMagnitude => (x * x) + (y * y) + (z * z);

        public double AngleBetween(Vector64 to)
        {
            return AngleBetween(this, to);
        }

        /// <summary>
        /// Shifts ALL THREE AXES by amount passed in.
        /// e.g. (5, 5, 3).ShiftAll(-2) returns (3, 3, 1)
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>A shifted copy of the original vector (DOES NOT AFFECT THE ORIGINAL VECTOR!)</returns>
        public Vector64 ShiftAll(double amount)
        {
            Vector64 copy = this;
            for (int i = 0; i <= 2; i++)
            {
                copy[i] += amount;
            }
            return copy;
        }

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException("Attempted to get a nonexistent value from a Vector64!");
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        return;
                    case 1:
                        y = value;
                        return;
                    case 2:
                        z = value;
                        return;
                    default:
                        throw new IndexOutOfRangeException("Attempted to set a nonexistent value from a Vector64!");
                }
            }
        }

        public override string ToString()
        {
            return $"V64({Math.Round(x, 3)}, {Math.Round(y, 3)}, {Math.Round(z, 3)})";
        }
    }
}