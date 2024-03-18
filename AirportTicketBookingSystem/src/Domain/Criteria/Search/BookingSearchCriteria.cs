namespace AirportTicketBookingSystem.Domain.Criteria.Search;

/// <summary>
/// Represents the criteria for searching bookings within the system,
/// allowing for filtering based on passenger identifier and associated flight details.
/// </summary>
public class BookingSearchCriteria
{
    public int? PassengerId { get; set; }
    public FlightSearchCriteria? Flight { get; set; }
}