using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Utility;

namespace AirportTicketBookingSystem.Domain;

public class Flight : IEntity
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; }

    public DateTime DepartureDate { get; }

    [StringLength(64, MinimumLength = 1, ErrorMessage = "Airport ID must be between 1 and 64 characters.")]
    public string DepartureAirportId { get; }

    [StringLength(64, MinimumLength = 1, ErrorMessage = "Airport ID must be between 1 and 64 characters.")]
    public string ArrivalAirportId { get; }

    public IReadOnlyDictionary<FlightClass, decimal> ClassPrices { get; }

    private Flight(
        int id, DateTime departureDate, string departureAirportId, string arrivalAirportId,
        IReadOnlyDictionary<FlightClass, decimal> classPrices)
    {
        Id = id;
        DepartureDate = departureDate;
        DepartureAirportId = departureAirportId;
        ArrivalAirportId = arrivalAirportId;
        ClassPrices = classPrices;
    }

    /// <exception cref="ValidationException">Thrown when any of the arguments do not meet the validation criteria.</exception>
    // you can add this method to FlightService and call the validation method from there
    // avoid adding validation methods to the domain model
    public static Flight Create(
        int id, DateTime departureDate, string departureAirportId, string arrivalAirportId,
        IReadOnlyDictionary<FlightClass, decimal> classPrices)
    {
        var obj = new Flight(id, departureDate, departureAirportId, arrivalAirportId, classPrices);
        obj.ValidateObjectOrThrow();
        return obj;
    }

    public override bool Equals(object? obj) =>
        obj is Flight other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString()
    {
        var pricesString = string.Join(", ", ClassPrices.Select(cp => $"{cp.Key}: {cp.Value:C}"));
        return $"Flight - ID: {Id}, Departure: {DepartureAirportId} on {DepartureDate:yyyy-MM-dd HH:mm}" +
               $", Arrival: {ArrivalAirportId}, Prices: [{pricesString}]";
    }
}