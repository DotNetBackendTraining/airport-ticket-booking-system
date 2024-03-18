using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Utility;

namespace AirportTicketBookingSystem.Domain;

public class Passenger : IEntity
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; }

    private Passenger(int id)
    {
        Id = id;
    }

    /// <exception cref="ValidationException">Thrown when any of the arguments do not meet the validation criteria.</exception>
    public static Passenger Create(int id)
    {
        var obj = new Passenger(id);
        obj.ValidateObjectOrThrow();
        return obj;
    }

    public override bool Equals(object? obj) =>
        obj is Passenger other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"Passenger - ID: {Id}";
}