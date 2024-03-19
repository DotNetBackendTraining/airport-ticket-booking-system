using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Presentation.MenuSystem;
using AirportTicketBookingSystem.Presentation.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ClientController(IServiceProvider serviceProvider)
    : GlobalController(serviceProvider)
{
    private IClientService ClientService => Provider.GetRequiredService<IClientService>();

    private int? _passengerId;
    private int PassengerId => _passengerId ??= IsRegistered();

    private int IsRegistered()
    {
        var res = PromptHelper.TryPromptForInput("Please Enter Your Passenger ID:  ", int.Parse, out var id);
        if (res && ClientService.IsPassengerRegistered(id)) return id;

        Console.WriteLine("You Can't Continue Without Your Passenger ID!");
        Environment.Exit(0);
        return id;
    }

    public void Start()
    {
        Console.WriteLine($"--- Logged In With ID: {PassengerId} ---");
        var menu = new Menu("Welcome to the Airport Ticket Booking System!", "Exit")
            .AddItem(new MenuItem("Search for Flights", SearchFlights))
            .AddItem(new MenuItem("Search for Airports", SearchAirports))
            .AddItem(new MenuItem("Book a Flight", BookFlight))
            .AddItem(ManageBookingsMenu());
        menu.Invoke();
    }

    private void BookFlight()
    {
        var res = PromptHelper.TryPromptForInput("Enter Flight ID:  ", int.Parse, out var flightId);
        if (!res) return;
        var flightClass = PromptDomain.FlightClass();
        if (flightClass == null) return;

        var booking = new Booking(flightId, PassengerId, flightClass.Value);
        var result = ClientService.AddBooking(booking);
        Display.OperationResult(result);
    }

    private Menu ManageBookingsMenu()
    {
        return new Menu("Bookings Actions")
            .AddItem(new MenuItem("Show All Bookings", ShowBookings))
            .AddItem(new MenuItem("Create Booking", BookFlight))
            .AddItem(new MenuItem("Modify Booking", ModifyBooking))
            .AddItem(new MenuItem("Cancel Booking", CancelBooking));
    }

    private void ShowBookings() =>
        Display.SearchResult(ClientService.GetAllBookings(PassengerId));

    private void ModifyBooking()
    {
        var booking = FindBooking();
        if (booking == null) return;

        Console.WriteLine("Let's choose a new flight class.");
        var flightClass = PromptDomain.FlightClass();
        if (flightClass == null)
        {
            Console.WriteLine("Nothing happened.");
            return;
        }

        booking = new Booking(booking.FlightId, booking.PassengerId, flightClass.Value);
        Display.OperationResult(ClientService.UpdateBooking(booking));
    }

    private void CancelBooking()
    {
        var booking = FindBooking();
        if (booking == null) return;

        var res = PromptHelper.PromptYesNo("Are you sure you want to delete this booking (y/n) ?  ");
        if (res) Display.OperationResult(ClientService.CancelBooking(booking));
        else Console.WriteLine("Nothing happened.");
    }

    private Booking? FindBooking()
    {
        Console.WriteLine("Which flight booking do you want to choose ?");
        var res = PromptHelper.TryPromptForInput("Please Enter The Flight ID:  ", int.Parse, out var flightId);
        if (!res) return null;

        var list = ClientService.GetAllBookings(PassengerId).Items
            .Where(b => b.FlightId == flightId).Take(1).ToList();
        if (list.Count > 0) return list[0];

        Console.WriteLine("Flight with this ID was not found in your bookings!");
        return null;
    }
}