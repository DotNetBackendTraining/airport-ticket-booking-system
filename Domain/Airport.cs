namespace AirportTicketBookingSystem.Domain;

public class Airport(string name, string country)
{
    public string Name { get; } = name;
    public string Country { get; } = country;

    public override bool Equals(object? obj) =>
        obj is Airport other &&
        Name == other.Name &&
        Country == other.Country;

    public override int GetHashCode() =>
        HashCode.Combine(Name, Country);
}