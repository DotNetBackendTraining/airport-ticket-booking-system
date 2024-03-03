using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Criteria;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class FlightRepository(
    ISimpleDatabaseService<Flight> databaseService,
    IAirportRepository airportRepository
) : IFlightRepository
{
    private ISimpleDatabaseService<Flight> DatabaseService { get; } = databaseService;

    private IAirportRepository AirportRepository { get; } = airportRepository;

    public void Add(Flight flight)
    {
        DatabaseService.Add(flight);
    }

    public Task AddAllAsync(IEnumerable<Flight> flights)
    {
        foreach (var flight in flights) DatabaseService.Add(flight);
        return Task.CompletedTask;
    }

    public Flight? GetById(int flightId)
    {
        return DatabaseService
            .GetAll()
            .FirstOrDefault(f => f.Id == flightId);
    }

    public IEnumerable<Flight> Search(FlightSearchCriteria criteria)
    {
        return Filter(DatabaseService.GetAll(), criteria);
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
        var airport = AirportRepository.GetById(airportId);
        return AirportRepository.Filter([airport], airportCriteria).Any();
    }
}