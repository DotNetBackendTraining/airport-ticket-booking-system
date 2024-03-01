namespace AirportTicketBookingSystem.Domain;

public class Airport(string name, string country)
{
    public string Name { get; set; } = name;
    public string Country { get; set; } = country;
}