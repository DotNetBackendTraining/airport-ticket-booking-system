using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Presentation.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class ClientController(IServiceProvider serviceProvider)
    : GlobalController(serviceProvider)
{
    private IClientService ClientService => Provider.GetRequiredService<IClientService>();

    private int? _passengerId;
    private int PassengerId => _passengerId ??= Authenticate();

    private int Authenticate()
    {
        var res = PromptHelper.TryPromptForInput("Please Enter Your Passenger ID:  ", int.Parse, out var id);
        if (res && ClientService.AuthenticatePassenger(id)) return id;

        Console.WriteLine("You Can't Continue Without Your Passenger ID!");
        Environment.Exit(0);
        return id;
    }

    public void Start()
    {
        Console.WriteLine($"--- Logged In With ID: {PassengerId} ---");
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