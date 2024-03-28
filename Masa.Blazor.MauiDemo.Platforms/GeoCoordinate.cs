namespace Masa.Blazor.MauiDemo.Platforms;

public class GeoCoordinate
{
    /// <summary>
    /// Gets or sets the latitude coordinate of this location.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of this location.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets the altitude in meters (if available) in a reference system which is specified by <see cref="AltitudeReferenceSystem"/>.
    /// </summary>
    /// <remarks>Returns 0 or <see langword="null"/> if not available.</remarks>
    public double? Altitude { get; set; }

    public static GeoCoordinate Beijing => new() { Latitude = 39.9042, Longitude = 116.4074 };
}