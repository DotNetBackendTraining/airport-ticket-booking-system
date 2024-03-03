using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Domain;

public class Flight(
    int id,
    DateTime departureDate,
    string departureAirportId,
    string arrivalAirportId)
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; } = id;

    public DateTime DepartureDate { get; set; } = departureDate;

    [StringLength(64, MinimumLength = 1, ErrorMessage = "Airport ID must be between 1 and 64 characters.")]
    public string DepartureAirportId { get; set; } = departureAirportId;

    [StringLength(64, MinimumLength = 1, ErrorMessage = "Airport ID must be between 1 and 64 characters.")]
    public string ArrivalAirportId { get; set; } = arrivalAirportId;

    public Dictionary<FlightClass, decimal> ClassPrices { get; set; } = [];

    public override bool Equals(object? obj) =>
        obj is Flight other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}