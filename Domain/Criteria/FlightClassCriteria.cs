namespace AirportTicketBookingSystem.Domain.Criteria;

public class FlightClassCriteria(FlightClass flightClass)
{
    public FlightClass Class { get; set; } = flightClass;
    public decimal? MaxPrice { get; set; }
}