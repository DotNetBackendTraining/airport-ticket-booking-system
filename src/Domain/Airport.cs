using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Utility;

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

    private Airport(string id, string name, string country)
    {
        Id = id;
        Name = name;
        Country = country;
    }

    /// <exception cref="ValidationException">Thrown when any of the arguments do not meet the validation criteria.</exception>
    public static Airport Create(string id, string name, string country)
    {
        var obj = new Airport(id, name, country);
        ValidationHelper.ValidateObjectOrThrow(obj);
        return obj;
    }

    public override bool Equals(object? obj) =>
        obj is Airport other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() =>
        $"Airport - ID: {Id}, Name: {Name}, Country: {Country}";
}