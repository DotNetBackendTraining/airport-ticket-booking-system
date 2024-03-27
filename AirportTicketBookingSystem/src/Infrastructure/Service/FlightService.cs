using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;
    private readonly IAirportService _airportService;

    public FlightService(IFlightRepository repository, IAirportService airportService)
    {
        _repository = repository;
        _airportService = airportService;
    }

    public void Add(Flight flight)
    {
        foreach (var id in new[] { flight.DepartureAirportId, flight.ArrivalAirportId })
            if (_airportService.GetById(id) == null)
                throw new DatabaseRelationalException($"Airport with ID '{id}' was not found in the repository");
        _repository.Add(flight);
    }

    public Flight? GetById(int flightId) => _repository.GetById(flightId);

    public IEnumerable<Flight> Search(FlightSearchCriteria criteria)
    {
        return Filter(_repository.GetAll(), criteria);
    }

    public IEnumerable<Flight> Filter(IEnumerable<Flight> flights, FlightSearchCriteria criteria)
    {
        if (criteria.ClassList != null)
            flights = flights.Where(f => MatchesClassList(f, criteria.ClassList));

        if (criteria.DepartureDate != null)
            flights = flights.Where(f => MatchesDepartureDate(f, criteria.DepartureDate));

        if (criteria.DepartureAirport != null)
            flights = flights.Where(f => MatchesAirport(f.DepartureAirportId, criteria.DepartureAirport));

        if (criteria.ArrivalAirport != null)
            flights = flights.Where(f => MatchesAirport(f.ArrivalAirportId, criteria.ArrivalAirport));

        return flights;
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
        return _airportService.Filter([airport], airportCriteria).Any();
    }
}