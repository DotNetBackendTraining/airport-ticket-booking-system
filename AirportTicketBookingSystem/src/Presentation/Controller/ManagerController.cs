using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Presentation.MenuSystem;
using AirportTicketBookingSystem.Presentation.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ManagerController(IServiceProvider serviceProvider)
    : GlobalController(serviceProvider)
{
    private IManagerService ManagerService => Provider.GetRequiredService<IManagerService>();

    public void Start()
    {
        var menu = new Menu("Welcome to the Airport Ticket Booking System!", "Exit")
            .AddItem(new MenuItem("Search for Bookings", SearchBookings))
            .AddItem(new MenuItem("Search for Flights", SearchFlights))
            .AddItem(new MenuItem("Search for Airports", SearchAirports))
            .AddItem(new MenuItem("Import Flights from a File", ImportFlights))
            .AddItem(PrintValidationsMenu());
        menu.Invoke();
    }

    private void SearchBookings()
    {
        var criteria = PromptFilter.PromptBookingFilter();
        var bookings = ManagerService.SearchBookings(criteria);
        Display.SearchResult(bookings);
    }

    private void ImportFlights()
    {
        Console.WriteLine("Enter CSV File (Full) Path:");
        var filepath = Console.ReadLine() ?? string.Empty;
        try
        {
            var results = ManagerService.BatchUploadFlights(filepath).ToList();
            Display.BatchOperationResults(results);
            if (results.All(res => !res.Success)) return;

            var save = PromptHelper.PromptYesNo("Would you like to save valid flights to system (y/n)?  ");
            if (!save) return;

            var saveResults = results
                .Select(r => r.Item)
                .OfType<Flight>()
                .Select(f => ManagerService.AddFlight(f));
            Display.BatchOperationResults(saveResults);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found!");
        }
    }

    private Menu PrintValidationsMenu()
    {
        var menu = new Menu("Available Models in the System");
        foreach (var type in ManagerService.GetDomainEntities())
        {
            menu.AddItem(new MenuItem(
                type.Name, () => Console.WriteLine(ManagerService.ReportConstraints(type))));
        }

        return menu;
    }
}