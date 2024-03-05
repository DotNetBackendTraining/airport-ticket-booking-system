using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Presentation.Utility;

public class PromptDomain
{
    public static FlightClass? FlightClass()
    {
        const string message = "Enter flight class (Economy, Business, FirstClass): ";
        if (!PromptHelper.TryPromptForInput(message, Enum.Parse<FlightClass>, out var flightClass))
            return null;
        return flightClass;
    }
}