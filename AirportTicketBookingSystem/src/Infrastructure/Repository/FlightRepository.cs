using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class FlightRepository : IFlightRepository
{
    private readonly IQueryDatabaseService<Flight> _queryDatabaseService;
    private readonly ICrudDatabaseService<Flight> _crudDatabaseService;

    public FlightRepository(
        IQueryDatabaseService<Flight> queryDatabaseService,
        ICrudDatabaseService<Flight> crudDatabaseService)
    {
        _queryDatabaseService = queryDatabaseService;
        _crudDatabaseService = crudDatabaseService;
    }

    public void Add(Flight flight) => _crudDatabaseService.AddAsync(flight);

    public IEnumerable<Flight> GetAll() => _queryDatabaseService.GetAll();

    public Flight? GetById(int flightId) => _queryDatabaseService
        .GetAll()
        .FirstOrDefault(f => f.Id == flightId);
}