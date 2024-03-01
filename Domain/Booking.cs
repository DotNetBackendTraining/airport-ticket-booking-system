namespace AirportTicketBookingSystem.Domain;

public class Booking(Flight flight, Passenger passenger, FlightClass bookingClass)
{
    public Flight Flight { get; } = flight;
    public Passenger Passenger { get; } = passenger;
    public FlightClass BookingClass { get; set; } = bookingClass;

    public override bool Equals(object? obj) =>
        obj is Booking other &&
        Equals(Flight, other.Flight) &&
        Equals(Passenger, other.Passenger);

    public override int GetHashCode() =>
        HashCode.Combine(Flight, Passenger);
}