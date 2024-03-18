namespace AirportTicketBookingSystem.Domain.Criteria.Search;

/// <summary>
/// Represents the criteria for searching flights, allowing for specification of
/// flight class, date, and airport details.
/// </summary>
public class FlightSearchCriteria
{
    public List<FlightClassCriteria>? ClassList { get; set; }
    public DateCriteria? DepartureDate { get; set; }
    public AirportSearchCriteria? DepartureAirport { get; set; }
    public AirportSearchCriteria? ArrivalAirport { get; set; }
}