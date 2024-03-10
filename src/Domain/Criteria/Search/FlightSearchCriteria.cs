namespace AirportTicketBookingSystem.Domain.Criteria.Search;

/// <summary>
/// Represents the criteria for searching flights, allowing for specification of
/// flight class, date, and airport details.
/// </summary>
public class FlightSearchCriteria
{
    // you can delete the nullable operator here
    // because the properties are not nullable but can be assigned with null
    // so you can remove the nullable operator
    public List<FlightClassCriteria>? ClassList { get; set; }
    public DateCriteria? DepartureDate { get; set; }
    public AirportSearchCriteria? DepartureAirport { get; set; }
    public AirportSearchCriteria? ArrivalAirport { get; set; }
}