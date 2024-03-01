namespace AirportTicketBookingSystem.Domain;

public class Passenger(int id)
{
    public int Id { get; set; } = id;
    public List<Booking> Bookings { get; set; } = [];
}