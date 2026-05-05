using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Store.ValueObjects;

public sealed record Location
{
    public string Address { get; }
    public string City { get; }
    public string Region { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    private Location(
        string address,
        string city,
        string region,
        double latitude,
        double longitude)
    {
        Address = address;
        City = city;
        Region = region;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Result<Location, Error> Create(
        string address,
        string city,
        string region,
        double latitude,
        double longitude)
    {
        if (string.IsNullOrWhiteSpace(address))
            return Error.Validation("location.address.empty", "Address cannot be empty.");

        if (string.IsNullOrWhiteSpace(city))
            return Error.Validation("location.city.empty", "City cannot be empty.");

        if (string.IsNullOrWhiteSpace(region))
            return Error.Validation("location.region.empty", "Region cannot be empty.");

        if (latitude is < -90 or > 90)
            return Error.Validation("location.latitude.invalid", "Latitude must be between -90 and 90.");

        if (longitude is < -180 or > 180)
            return Error.Validation("location.longitude.invalid", "Longitude must be between -180 and 180.");

        return new Location(address, city, region, latitude, longitude);
    }
}
