﻿using LagoVista.Core.Geo;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.Core.Models.Geo
{
    public enum DistanceUnits
    {
        NauticalMiles,
        Miles,
        Kilometers,
        Meters
    }

    public class GeoLocation : IGeoLocation
    {
        public double Altitude { get; set; }
        public string LastUpdated { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /// <summary>
        /// Get distance between two points
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public double DistanceFrom(GeoLocation location, DistanceUnits distanceUnits = DistanceUnits.NauticalMiles)
        {
            double phi_s = Latitude.ToRadians(),
                   lamda_s = Longitude.ToRadians(),
                   phi_f = location.Latitude.ToRadians(),
                   lamda_f = location.Longitude.ToRadians();

            // using vincenty formula
            double y = Math.Sqrt(Math.Pow((Math.Cos(phi_f) * Math.Sin(lamda_s - lamda_f)), 2) + Math.Pow((Math.Cos(phi_s) *
                           Math.Sin(phi_f) - Math.Sin(phi_s) * Math.Cos(phi_f) * Math.Cos(lamda_s - lamda_f)), 2));
            double x = Math.Sin(phi_s) * Math.Sin(phi_f) + Math.Cos(phi_s) * Math.Cos(phi_f) * Math.Cos(lamda_s - lamda_f);
            double delta = Math.Atan2(y, x);
            var nm = delta.ToDegrees() * 60;

            switch(distanceUnits)
            {
                case DistanceUnits.Kilometers: return nm * 1.852;
                case DistanceUnits.Meters:return nm * 1852;
                case DistanceUnits.Miles: return nm * 1.15077945;
            }

            return nm;
        }

        public double HeadingTo(GeoLocation location)
        {
            var deltaLon = location.Longitude.ToRadians() - Longitude.ToRadians();

            var heading = Math.Atan2(deltaLon, Math.Cos(Latitude.ToRadians()) * Math.Sin(location.Latitude.ToRadians()) - Math.Sin(Latitude.ToRadians()) * Math.Cos(location.Latitude.ToRadians()) * Math.Cos(deltaLon)) * 180 / Math.PI;
            return (heading < 0) ? heading + 360.0 : heading;
        }

        public string ToNuvIoTFormat()
        {
            return $"{Latitude:0.0000000},{Longitude:0.0000000}";
        }



        public override string ToString()
        {
            return $"Latitude = {Latitude:0.0000000}; Longitude = {Longitude:0.0000000}";
        }
    }
}
