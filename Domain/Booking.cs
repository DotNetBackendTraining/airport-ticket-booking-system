namespace AirportTicketBookingSystem.Domain;

public class Booking(Flight flight, Passenger passenger)
{
    public Flight Flight { get; set; } = flight;
    public Passenger Passenger { get; set; } = passenger;
}