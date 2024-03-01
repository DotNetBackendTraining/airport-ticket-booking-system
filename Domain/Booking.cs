namespace AirportTicketBookingSystem.Domain;

public class Booking(int flightId, int passengerId, FlightClass bookingClass)
{
    public int FlightId { get; } = flightId;
    public int PassengerId { get; } = passengerId;
    public FlightClass BookingClass { get; set; } = bookingClass;

    public override bool Equals(object? obj) =>
        obj is Booking other &&
        FlightId == other.FlightId &&
        PassengerId == other.PassengerId;

    public override int GetHashCode() =>
        HashCode.Combine(FlightId, PassengerId);
}