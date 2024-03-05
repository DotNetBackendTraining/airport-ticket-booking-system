using AirportTicketBookingSystem.Presentation.Utility;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ManagerController(IServiceProvider serviceProvider)
    : GlobalController(serviceProvider)
{
    public void Start()
    {
        PromptMenu.ActionMenu("Welcome to the Airport Ticket Booking System!", [
            ("Search for Bookings", SearchBookings),
            ("Search for Flights", SearchFlights),
            ("Import Flights from a File", ImportFlights),
            ("Print Validation Constraints", PrintValidations)
        ]);
    }

    private void SearchBookings()
    {
        throw new NotImplementedException();
    }

    private void ImportFlights()
    {
        throw new NotImplementedException();
    }

    private void PrintValidations()
    {
        throw new NotImplementedException();
    }
}