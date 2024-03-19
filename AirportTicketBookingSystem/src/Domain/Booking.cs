using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Domain;

public class Booking : IEntity
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int FlightId { get; }

    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int PassengerId { get; }

    public FlightClass BookingClass { get; }

    public Booking(int flightId, int passengerId, FlightClass bookingClass)
    {
        FlightId = flightId;
        PassengerId = passengerId;
        BookingClass = bookingClass;
    }

    public override bool Equals(object? obj) =>
        obj is Booking other &&
        FlightId == other.FlightId &&
        PassengerId == other.PassengerId;

    public override int GetHashCode() =>
        HashCode.Combine(FlightId, PassengerId);

    public override string ToString() =>
        $"Booking - Flight ID: {FlightId}, Passenger ID: {PassengerId}, Class: {BookingClass}";
}