namespace AirportTicketBookingSystem.Domain;

public class Booking(Flight flight, Passenger passenger)
{
    public Flight Flight { get; } = flight;
    public Passenger Passenger { get; } = passenger;

    public override bool Equals(object? obj) =>
        obj is Booking other &&
        Equals(Flight, other.Flight) &&
        Equals(Passenger, other.Passenger);

    public override int GetHashCode() =>
        HashCode.Combine(Flight, Passenger);
}