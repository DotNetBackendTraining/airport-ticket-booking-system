using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Test.Domain;

/// <summary>
/// Empty entity for testing purposes
/// </summary>
public class Entity : IEntity
{
    public int Id { get; set; }

    public Entity(int id) => Id = id;

    public override bool Equals(object? obj) => obj is Entity other && other.Id == Id;

    public override int GetHashCode() => Id.GetHashCode();
}