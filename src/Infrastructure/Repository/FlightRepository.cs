using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class FlightRepository : IFlightRepository
{
    private readonly ISimpleDatabaseService<Flight> _databaseService;
    private readonly IAirportRepository _airportRepository;
    
    public FlightRepository(ISimpleDatabaseService<Flight> databaseService, IAirportRepository airportRepository)
    {
        _databaseService = databaseService;
        _airportRepository = airportRepository;
    }

    public void Add(Flight flight)
    {
        foreach (var id in new[] { flight.DepartureAirportId, flight.ArrivalAirportId })
            if (_airportRepository.GetById(id) == null)
                throw new InvalidOperationException($"Airport with ID '{id}' was not found in the repository");
        _databaseService.Add(flight);
    }

    public Flight? GetById(int flightId)
    {
        return _databaseService
            .GetAll()
            .FirstOrDefault(f => f.Id == flightId);
    }

    public IEnumerable<Flight> Search(FlightSearchCriteria criteria)
    {
        return Filter(_databaseService.GetAll(), criteria);
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
        var airport = _airportRepository.GetById(airportId);
        return airport != null && 
               _airportRepository.Filter(new List<Airport>{airport}, airportCriteria).Any();
    }
}