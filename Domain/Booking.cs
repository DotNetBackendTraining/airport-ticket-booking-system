using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Domain;

public class Booking(int flightId, int passengerId, FlightClass bookingClass)
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int FlightId { get; } = flightId;

    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int PassengerId { get; } = passengerId;

    public FlightClass BookingClass { get; set; } = bookingClass;

    public override bool Equals(object? obj) =>
        obj is Booking other &&
        FlightId == other.FlightId &&
        PassengerId == other.PassengerId;

    public override int GetHashCode() =>
        HashCode.Combine(FlightId, PassengerId);
}