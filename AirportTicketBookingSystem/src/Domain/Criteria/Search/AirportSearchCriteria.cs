namespace AirportTicketBookingSystem.Domain.Criteria.Search;

/// <summary>
/// Represents the criteria for searching airports within the system,
/// allowing for filtering based on airport name and location.
/// </summary>
public class AirportSearchCriteria
{
    public string? Name { get; set; }
    public string? Country { get; set; }
}