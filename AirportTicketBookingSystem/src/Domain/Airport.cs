using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Domain;

public class Airport : IEntity
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public string Id { get; }

    [Length(1, 256, ErrorMessage = "Name must be between 1 and 256 characters.")]
    public string Name { get; }

    [Length(1, 256, ErrorMessage = "Country must be between 1 and 256 characters.")]
    public string Country { get; }

    public Airport(string id, string name, string country)
    {
        Id = id;
        Name = name;
        Country = country;
    }

    public override bool Equals(object? obj) =>
        obj is Airport other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() =>
        $"Airport - ID: {Id}, Name: {Name}, Country: {Country}";
}