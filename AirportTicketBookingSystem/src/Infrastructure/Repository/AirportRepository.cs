using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository : IAirportRepository
{
    private IDatabaseService<Airport> DatabaseService { get; }
    public AirportRepository(IDatabaseService<Airport> databaseService) => DatabaseService = databaseService;

    public void Add(Airport airport) => DatabaseService.Add(airport);

    public IEnumerable<Airport> GetAll() => DatabaseService.GetAll();

    public Airport? GetById(string id) => DatabaseService
        .GetAll()
        .FirstOrDefault(a => a.Id == id);
}