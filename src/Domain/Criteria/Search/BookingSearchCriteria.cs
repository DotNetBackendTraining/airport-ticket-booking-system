namespace AirportTicketBookingSystem.Domain.Criteria.Search;

/// <summary>
/// Represents the criteria for searching bookings within the system,
/// allowing for filtering based on passenger identifier and associated flight details.
/// </summary>
public class BookingSearchCriteria
{
    public int? PassengerId { get; set; }
    
    // you can delete the nullable operator here
    // because the properties are not nullable but can be assigned with null
    // so you can remove the nullable operator
    public FlightSearchCriteria? Flight { get; set; }
}