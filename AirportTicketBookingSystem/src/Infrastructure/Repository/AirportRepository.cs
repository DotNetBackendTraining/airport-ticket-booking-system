using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository : IAirportRepository
{
    private readonly IDatabaseService<Airport> _databaseService;
    public AirportRepository(IDatabaseService<Airport> databaseService) => _databaseService = databaseService;

    public void Add(Airport airport) => _databaseService.Add(airport);

    public IEnumerable<Airport> GetAll() => _databaseService.GetAll();

    public Airport? GetById(string id) => _databaseService
        .GetAll()
        .FirstOrDefault(a => a.Id == id);
}