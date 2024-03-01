namespace AirportTicketBookingSystem.Domain;

public class Flight(
    int id,
    DateTime departureDate,
    Airport departureAirport,
    Airport arrivalAirport)
{
    public int Id { get; set; } = id;
    public DateTime DepartureDate { get; set; } = departureDate;
    public Airport DepartureAirport { get; set; } = departureAirport;
    public Airport ArrivalAirport { get; set; } = arrivalAirport;
    public Dictionary<FlightClass, decimal> ClassPrices { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
}