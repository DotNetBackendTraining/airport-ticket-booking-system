namespace AirportTicketBookingSystem.Domain.Criteria;

public class DateCriteria
{
    // you can delete the nullable operator here
    // because the properties are not nullable but can be assigned with null
    // so you can remove the nullable operator
    
    public DateTime? Min { get; set; }
    public DateTime? Max { get; set; }
}