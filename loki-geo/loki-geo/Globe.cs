using System;

namespace loki_geo
{
    /// <summary>
    /// A class representing the tools and numbers needed to interact with a spherical globe.
    /// </summary>
    public static class Globe
    {
        /// <summary>
        /// The radius of Earth, in meters (rounded)
        /// </summary>
        public const double EarthRadius = 6378137.0;
        public static double EarthCircumference => 2 * Math.PI * EarthRadius;

        /// <summary>
        /// Converts a Vector64 coordinate in world space to a LatLonCoord representing its position on the surface of a sphere.
        /// This assumes that (0,0,0) is the center of the globe.
        /// </summary>
        /// <param name="cartesian">Vector64 to be converted</param>
        /// <param name="radius">The radius of the reference sphere (defaults to EarthRadius)</param>
        /// <returns></returns>
        public static LatLonCoord XYZToLL(Vector64 cartesian, double radius = EarthRadius)
        {
            double altitude = cartesian.magnitude - radius;
            Vector64 normalized = cartesian.normalized;

            double lat_rad = Math.Asin(normalized.z);
            double lon_rad = Math.Acos(normalized.x / Math.Cos(lat_rad)) * Math.Sign(normalized.y);

            return new LatLonCoord { Lat_Rad = lat_rad, Lon_Rad = lon_rad, Alt = altitude };
        }

        /// <summary>
        /// Converts a LatLonCoord representing an object on the surface of a sphere to a Vector64.
        /// This assumes that (0,0,0) is the center of the globe.
        /// </summary>
        /// <param name="latLon">LatLonCoord to be converted</param>
        /// <param name="radius">The radius of the reference sphere (defaults to EarthRadius)</param>
        /// <returns></returns>
        public static Vector64 LLToXYZ(LatLonCoord latLon, double radius = EarthRadius)
        {
            double magnitude = latLon.Alt + radius;
            double cosLat = Math.Cos(latLon.Lat_Rad);

            double x = cosLat * Math.Cos(latLon.Lon_Rad);
            double y = cosLat * Math.Sin(latLon.Lon_Rad);
            double z = Math.Sin(latLon.Lat_Rad);

            return new Vector64(x, y, z) * magnitude;
        }
    }
}
