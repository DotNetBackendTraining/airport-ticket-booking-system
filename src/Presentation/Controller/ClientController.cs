using AirportTicketBookingSystem.Presentation.Utility;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ClientController(IServiceProvider serviceProvider)
    : GlobalController(serviceProvider)
{
    public void Start()
    {
        PromptMenu.ActionMenu("Welcome to the Airport Ticket Booking System!", [
            ("Search for Flights", SearchFlights),
            ("Book a Flight", BookFlight),
            ("Manage Bookings", ManageBookings)
        ]);
    }

    private void BookFlight()
    {
        throw new NotImplementedException();
    }

    private void ManageBookings()
    {
        throw new NotImplementedException();
    }
}