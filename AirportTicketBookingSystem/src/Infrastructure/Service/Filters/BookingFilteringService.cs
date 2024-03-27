using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service.Filters;

public class BookingFilteringService : IFilteringService<Booking, BookingSearchCriteria>
{
    private readonly IFlightService _flightService;
    private readonly IFilteringService<Flight, FlightSearchCriteria> _flightFilteringService;

    public BookingFilteringService(
        IFlightService flightService,
        IFilteringService<Flight, FlightSearchCriteria> flightFilteringService)
    {
        _flightService = flightService;
        _flightFilteringService = flightFilteringService;
    }

    public IEnumerable<Booking> Filter(IEnumerable<Booking> entities, BookingSearchCriteria criteria)
    {
        if (criteria.PassengerId.HasValue)
            entities = entities.Where(b => b.PassengerId == criteria.PassengerId.Value);

        if (criteria.Flight != null)
            entities = entities.Where(b => MatchingFlight(b.FlightId, criteria.Flight));

        return entities;
    }

    private bool MatchingFlight(int flightId, FlightSearchCriteria flightCriteria)
    {
        var flight = _flightService.GetById(flightId);
        return _flightFilteringService.Filter([flight], flightCriteria).Any();
    }
}