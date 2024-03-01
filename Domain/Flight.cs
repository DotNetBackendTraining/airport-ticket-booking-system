namespace AirportTicketBookingSystem.Domain;

public class Flight(
    int id,
    DateTime departureDate,
    string departureAirportId,
    string arrivalAirportId)
{
    public int Id { get; } = id;
    public DateTime DepartureDate { get; set; } = departureDate;
    public string DepartureAirportId { get; set; } = departureAirportId;
    public string ArrivalAirportId { get; set; } = arrivalAirportId;
    public Dictionary<FlightClass, decimal> ClassPrices { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];

    public override bool Equals(object? obj) =>
        obj is Flight other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}