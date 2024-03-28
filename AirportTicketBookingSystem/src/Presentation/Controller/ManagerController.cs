using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Presentation.MenuSystem;
using AirportTicketBookingSystem.Presentation.Utility;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ManagerController : GlobalController
{
    private readonly IManagerService _managerService;

    public ManagerController(
        IGlobalService globalService,
        IManagerService managerService)
        : base(globalService)
    {
        _managerService = managerService;
    }

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
        var bookings = _managerService.SearchBookings(criteria);
        Display.SearchResult(bookings);
    }

    private void ImportFlights()
    {
        Console.WriteLine("Enter CSV File (Full) Path:");
        var filepath = Console.ReadLine() ?? string.Empty;
        try
        {
            var results = _managerService.BatchUploadFlights(filepath).ToList();
            Display.BatchOperationResults(results);
            if (results.All(res => !res.Success)) return;

            var save = PromptHelper.PromptYesNo("Would you like to save valid flights to system (y/n)?  ");
            if (!save) return;

            var saveResults = results
                .Select(r => r.Item)
                .OfType<Flight>()
                .Select(f => _managerService.AddFlight(f));
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
        foreach (var type in _managerService.GetDomainEntities())
        {
            menu.AddItem(new MenuItem(
                type.Name, () => Console.WriteLine(_managerService.ReportConstraints(type))));
        }

        return menu;
    }
}