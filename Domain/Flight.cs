namespace AirportTicketBookingSystem.Domain;

public class Flight(
    int id,
    DateTime departureDate,
    Airport departureAirport,
    Airport arrivalAirport)
{
    public int Id { get; } = id;
    public DateTime DepartureDate { get; set; } = departureDate;
    public Airport DepartureAirport { get; set; } = departureAirport;
    public Airport ArrivalAirport { get; set; } = arrivalAirport;
    public Dictionary<FlightClass, decimal> ClassPrices { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];

    public override bool Equals(object? obj) =>
        obj is Flight other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}