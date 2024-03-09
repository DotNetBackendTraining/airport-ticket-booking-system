namespace AirportTicketBookingSystem.Domain.Criteria;

public class FlightClassCriteria
{
    public FlightClassCriteria(FlightClass flightClass)
    {
        Class = flightClass;
    }
    public FlightClass Class { get; set; }
    public decimal? MaxPrice { get; set; }
}