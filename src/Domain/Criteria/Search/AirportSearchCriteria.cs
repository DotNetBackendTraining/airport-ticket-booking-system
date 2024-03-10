namespace AirportTicketBookingSystem.Domain.Criteria.Search;

/// <summary>
/// Represents the criteria for searching airports within the system,
/// allowing for filtering based on airport name and location.
/// </summary>
public class AirportSearchCriteria
{
    // you can delete the nullable operator here
    // because the properties are not nullable but can be assigned with null
    // so you can remove the nullable operator
    public string? Name { get; set; }
    public string? Country { get; set; }
}