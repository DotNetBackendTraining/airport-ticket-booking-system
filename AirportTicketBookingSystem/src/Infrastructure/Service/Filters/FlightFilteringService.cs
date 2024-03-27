using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service.Filters;

public class FlightFilteringService : IFilteringService<Flight, FlightSearchCriteria>
{
    private readonly IAirportService _airportService;
    private readonly IFilteringService<Airport, AirportSearchCriteria> _airportFilteringService;

    public FlightFilteringService(
        IAirportService airportService,
        IFilteringService<Airport, AirportSearchCriteria> airportFilteringService)
    {
        _airportService = airportService;
        _airportFilteringService = airportFilteringService;
    }

    public IEnumerable<Flight> Filter(IEnumerable<Flight> entities, FlightSearchCriteria criteria)
    {
        if (criteria.ClassList != null)
            entities = entities.Where(f => MatchesClassList(f, criteria.ClassList));

        if (criteria.DepartureDate != null)
            entities = entities.Where(f => MatchesDepartureDate(f, criteria.DepartureDate));

        if (criteria.DepartureAirport != null)
            entities = entities.Where(f => MatchesAirport(f.DepartureAirportId, criteria.DepartureAirport));

        if (criteria.ArrivalAirport != null)
            entities = entities.Where(f => MatchesAirport(f.ArrivalAirportId, criteria.ArrivalAirport));

        return entities;
    }

    private static bool MatchesClassList(Flight flight, IEnumerable<FlightClassCriteria> classList)
    {
        return classList.All(classCriteria =>
            flight.ClassPrices.ContainsKey(classCriteria.Class) &&
            (classCriteria.MaxPrice == null || flight.ClassPrices[classCriteria.Class] <= classCriteria.MaxPrice));
    }

    private static bool MatchesDepartureDate(Flight flight, DateCriteria dateCriteria)
    {
        return (dateCriteria.Min ?? DateTime.MinValue) <= flight.DepartureDate &&
               flight.DepartureDate <= (dateCriteria.Max ?? DateTime.MaxValue);
    }

    private bool MatchesAirport(string airportId, AirportSearchCriteria airportCriteria)
    {
        var airport = _airportService.GetById(airportId);
        return _airportFilteringService.Filter([airport], airportCriteria).Any();
    }
}