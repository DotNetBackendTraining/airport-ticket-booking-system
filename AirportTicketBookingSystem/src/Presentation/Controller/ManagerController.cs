using AirportTicketBookingSystem.Application.Interfaces.Service.Request;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Presentation.MenuSystem;
using AirportTicketBookingSystem.Presentation.Utility;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ManagerController : GlobalController
{
    private readonly IManagerRequestService _managerRequestService;

    public ManagerController(
        IGlobalRequestService globalRequestService,
        IManagerRequestService managerRequestService)
        : base(globalRequestService)
    {
        _managerRequestService = managerRequestService;
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
        var bookings = _managerRequestService.SearchBookings(criteria);
        Display.SearchResult(bookings);
    }

    private void ImportFlights()
    {
        Console.WriteLine("Enter CSV File (Full) Path:");
        var filepath = Console.ReadLine() ?? string.Empty;
        try
        {
            var results = _managerRequestService.BatchUploadFlights(filepath).ToList();
            Display.BatchOperationResults(results);
            if (results.All(res => !res.Success)) return;

            var save = PromptHelper.PromptYesNo("Would you like to save valid flights to system (y/n)?  ");
            if (!save) return;

            var saveTasks = results
                .Select(r => r.Item)
                .OfType<Flight>()
                .Select(f => _managerRequestService.AddFlightAsync(f))
                .ToList();

            saveTasks.ForEach(t => t.Wait());
            Display.BatchOperationResults(saveTasks.Select(t => t.Result));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found!");
        }
    }

    private Menu PrintValidationsMenu()
    {
        var menu = new Menu("Available Models in the System");
        foreach (var type in _managerRequestService.GetDomainEntities())
        {
            menu.AddItem(new MenuItem(
                type.Name, () => Console.WriteLine(_managerRequestService.ReportConstraints(type))));
        }

        return menu;
    }
}