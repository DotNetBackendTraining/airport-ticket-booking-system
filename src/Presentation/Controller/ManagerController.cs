using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Domain;
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