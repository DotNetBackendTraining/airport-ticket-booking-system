using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Presentation.Utility;

public static class PromptCriteria
{
    public static DateCriteria DateCriteria()
    {
        DateCriteria criteria = new();

        if (PromptHelper.TryPromptForInput(
                "Enter minimum date (yyyy-MM-dd) or press Enter to skip: ", DateTime.Parse, out var minDate))
            criteria.Min = minDate;

        if (PromptHelper.TryPromptForInput(
                "Enter maximum date (yyyy-MM-dd) or press Enter to skip: ", DateTime.Parse, out var maxDate))
            criteria.Max = maxDate;

        return criteria;
    }

    public static List<FlightClassCriteria> FlightClassCriteriaList()
    {
        var flightClassCriteriaList = new List<FlightClassCriteria>();
        var addMore = true;

        while (addMore)
        {
            Console.WriteLine("\n--- Add Flight Class Criteria ---");
            var flightClass = PromptDomain.FlightClass();
            if (flightClass == null) break;

            var hasMaxPrice = PromptHelper.TryPromptForInput(
                "Enter maximum price or press Enter to skip: ", decimal.Parse, out var maxPrice);

            var criteria = hasMaxPrice
                ? new FlightClassCriteria(flightClass.Value) { MaxPrice = maxPrice }
                : new FlightClassCriteria(flightClass.Value);

            flightClassCriteriaList.Add(criteria);

            addMore = PromptHelper.PromptYesNo("Do you want to add more flight class criteria (y/n)? ");
        }

        return flightClassCriteriaList;
    }

    public static AirportSearchCriteria? AirportCriteria()
    {
        Console.Write("Enter airport name or press Enter to skip: ");
        var name = Console.ReadLine();

        Console.Write("Enter airport country or press Enter to skip: ");
        var country = Console.ReadLine();

        if (string.IsNullOrEmpty(null) && string.IsNullOrEmpty(country)) return null;
        return new AirportSearchCriteria { Name = name, Country = country };
    }
}