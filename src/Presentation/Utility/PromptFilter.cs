using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Presentation.Utility;

public static class PromptFilter
{
    public static FlightSearchCriteria PromptFlightFilter()
    {
        var flightSearchCriteria = new FlightSearchCriteria();
        var addingFilters = true;

        while (addingFilters)
        {
            Console.WriteLine("\n--- Flight Search Options ---");
            Console.WriteLine("1. Add Departure Date Filter");
            Console.WriteLine("2. Add Flight Class Filter");
            Console.WriteLine("3. Add Departure Airport Filter");
            Console.WriteLine("4. Add Arrival Airport Filter");
            Console.WriteLine("5. Show Results");

            Console.Write("Select an option or press Enter to show results: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    flightSearchCriteria.DepartureDate = PromptCriteria.DateCriteria();
                    break;
                case "2":
                    var flightClassCriteriaList = PromptCriteria.FlightClassCriteriaList();
                    if (flightClassCriteriaList.Count > 0)
                        flightSearchCriteria.ClassList = flightClassCriteriaList;
                    break;
                case "3":
                    flightSearchCriteria.DepartureAirport = PromptCriteria.AirportCriteria();
                    break;
                case "4":
                    flightSearchCriteria.ArrivalAirport = PromptCriteria.AirportCriteria();
                    break;
                case "5":
                case "":
                    addingFilters = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }

        return flightSearchCriteria;
    }
}