using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Presentation.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ManagerController(IServiceProvider serviceProvider)
    : GlobalController(serviceProvider)
{
    private IManagerService ManagerService => Provider.GetRequiredService<IManagerService>();

    public void Start()
    {
        PromptMenu.ActionMenu("Welcome to the Airport Ticket Booking System!", [
            ("Search for Bookings", SearchBookings),
            ("Search for Flights", SearchFlights),
            ("Search for Airports", SearchAirports),
            ("Import Flights from a File", ImportFlights),
            ("Print Validation Constraints", PrintValidations)
        ]);
    }

    private void SearchBookings()
    {
        var criteria = PromptFilter.PromptBookingFilter();
        var bookings = ManagerService.SearchBookings(criteria);
        Display.SearchResult(bookings);
    }

    private void ImportFlights()
    {
        throw new NotImplementedException();
    }

    private void PrintValidations()
    {
        var types = ManagerService.GetDomainEntities();
        var menuOptions = types.Select(type =>
        {
            return (type.Name, (Action)PrintReport);

            void PrintReport() => Console.WriteLine(ManagerService.ReportConstraints(type));
        });

        PromptMenu.ActionMenu("Available Models in the System", menuOptions.ToList(), "Go Back");
    }
}