using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Domain;

public class Airport(string id, string name, string country)
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public string Id { get; } = id;

    [Length(1, 256, ErrorMessage = "Name must be between 1 and 256 characters.")]
    public string Name { get; set; } = name;

    [Length(1, 256, ErrorMessage = "Country must be between 1 and 256 characters.")]
    public string Country { get; set; } = country;

    public override bool Equals(object? obj) =>
        obj is Airport other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}