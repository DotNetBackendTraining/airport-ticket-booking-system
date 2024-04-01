using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository : IAirportRepository
{
    private readonly IQueryDatabaseService<Airport> _queryDatabaseService;
    private readonly ICrudDatabaseService<Airport> _crudDatabaseService;

    public AirportRepository(
        IQueryDatabaseService<Airport> queryDatabaseService,
        ICrudDatabaseService<Airport> crudDatabaseService)
    {
        _queryDatabaseService = queryDatabaseService;
        _crudDatabaseService = crudDatabaseService;
    }

    public async Task AddAsync(Airport airport) => await _crudDatabaseService.AddAsync(airport);

    public IEnumerable<Airport> GetAll() => _queryDatabaseService.GetAll();

    public Airport? GetById(string id) => _queryDatabaseService
        .GetAll()
        .FirstOrDefault(a => a.Id == id);
}