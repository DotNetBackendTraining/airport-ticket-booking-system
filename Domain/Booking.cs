using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Utility;

namespace AirportTicketBookingSystem.Domain;

public class Booking
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int FlightId { get; }

    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int PassengerId { get; }

    public FlightClass BookingClass { get; }

    private Booking(int flightId, int passengerId, FlightClass bookingClass)
    {
        FlightId = flightId;
        PassengerId = passengerId;
        BookingClass = bookingClass;
    }

    /// <exception cref="ValidationException">Thrown when any of the arguments do not meet the validation criteria.</exception>
    public static Booking Create(int flightId, int passengerId, FlightClass bookingClass)
    {
        var obj = new Booking(flightId, passengerId, bookingClass);
        ValidationHelper.ValidateObjectOrThrow(obj);
        return obj;
    }

    public override bool Equals(object? obj) =>
        obj is Booking other &&
        FlightId == other.FlightId &&
        PassengerId == other.PassengerId;

    public override int GetHashCode() =>
        HashCode.Combine(FlightId, PassengerId);
}