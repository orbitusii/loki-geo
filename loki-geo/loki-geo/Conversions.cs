using System;

namespace loki_geo
{
    public static class Conversions
    {
        public const double ToRadians = Math.PI / 180;
        public const double ToDegrees = 1 / ToRadians;
        public const double MetersToNM = 5.399568e-4;
        public const double MetersToFeet = 3.28084;
        public const double MetersPerSecToKnots = 1.944012;
    }
}
