using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Domain;

public class Passenger : IEntity
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; }

    public Passenger(int id)
    {
        Id = id;
    }

    public override bool Equals(object? obj) =>
        obj is Passenger other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"Passenger - ID: {Id}";
}