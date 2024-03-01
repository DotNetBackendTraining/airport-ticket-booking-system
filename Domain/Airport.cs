namespace AirportTicketBookingSystem.Domain;

public class Airport(string id, string name, string country)
{
    public string Id { get; } = id;
    public string Name { get; set; } = name;
    public string Country { get; set; } = country;

    public override bool Equals(object? obj) =>
        obj is Airport other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}