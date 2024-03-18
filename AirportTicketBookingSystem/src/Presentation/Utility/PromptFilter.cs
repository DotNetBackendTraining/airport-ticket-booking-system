using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Presentation.MenuSystem;

namespace AirportTicketBookingSystem.Presentation.Utility;

public static class PromptFilter
{
    public static FlightSearchCriteria PromptFlightFilter(string returnMessage = "Show Results")
    {
        var criteria = new FlightSearchCriteria();
        var menu = new Menu("--- Flight Search Options ---", returnMessage)
            .AddItem(new MenuItem("Add Flight Class Filter", Action1))
            .AddItem(new MenuItem("Add Departure Date Filter", Action2))
            .AddItem(new MenuItem("Add Departure Airport Filter", Action3))
            .AddItem(new MenuItem("Add Arrival Airport Filter", Action4));
        menu.Invoke();
        return criteria;

        void Action1()
        {
            var list = PromptCriteria.FlightClassCriteriaList();
            if (list.Count > 0) criteria.ClassList = list;
        }

        void Action2() => criteria.DepartureDate = PromptCriteria.DateCriteria();
        void Action3() => criteria.DepartureAirport = PromptCriteria.AirportCriteria();
        void Action4() => criteria.ArrivalAirport = PromptCriteria.AirportCriteria();
    }

    public static BookingSearchCriteria PromptBookingFilter()
    {
        var criteria = new BookingSearchCriteria();
        var menu = new Menu("--- Booking Search Options ---", "Show Results")
            .AddItem(new MenuItem("Specify Passenger ID", Action1))
            .AddItem(new MenuItem("Add Flight Filters", Action2));
        menu.Invoke();
        return criteria;

        void Action1()
        {
            PromptHelper.TryPromptForInput("Enter Passenger ID:  ", int.Parse, out var id);
            criteria.PassengerId = id;
        }

        void Action2() => criteria.Flight = PromptFlightFilter("Go Back");
    }
}