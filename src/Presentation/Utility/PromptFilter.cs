using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Presentation.Utility;

public static class PromptFilter
{
    public static FlightSearchCriteria PromptFlightFilter(string exitOptionMessage = "Show Results")
    {
        var criteria = new FlightSearchCriteria();
        PromptMenu.ActionMenu("--- Flight Search Options ---", [
            ("Add Flight Class Filter", Action1),
            ("Add Departure Date Filter", Action2),
            ("Add Departure Airport Filter", Action3),
            ("Add Arrival Airport Filter", Action4)
        ], exitOptionMessage);
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
        PromptMenu.ActionMenu("--- Booking Search Options ---", [
            ("Specify Passenger ID", Action1),
            ("Add Flight Filters", Action2),
        ], "Show Results");
        return criteria;

        void Action1()
        {
            PromptHelper.TryPromptForInput("Enter Passenger ID:  ", int.Parse, out var id);
            criteria.PassengerId = id;
        }

        void Action2() => criteria.Flight = PromptFlightFilter("Go Back");
    }
}