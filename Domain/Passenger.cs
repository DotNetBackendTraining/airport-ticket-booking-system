using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Domain;

public class Passenger(int id)
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; } = id;

    public override bool Equals(object? obj) =>
        obj is Passenger other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}