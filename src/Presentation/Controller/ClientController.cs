using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Domain;
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
        var res = PromptHelper.TryPromptForInput("Enter Flight ID:  ", int.Parse, out var flightId);
        if (!res) return;
        var flightClass = PromptDomain.FlightClass();
        if (flightClass == null) return;

        try
        {
            var booking = Booking.Create(flightId, PassengerId, flightClass.Value);
            var result = ClientService.AddBooking(booking);
            Display.OperationResult(result);
        }
        catch (ValidationException e)
        {
            Console.WriteLine("Booking creation failed:  " + e.Message);
        }
    }

    private void ManageBookings()
    {
        Display.SearchResult(ClientService.GetAllBookings(PassengerId));
    }
}