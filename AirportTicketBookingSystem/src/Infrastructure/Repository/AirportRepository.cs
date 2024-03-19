using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository(
    IDatabaseService<Airport> databaseService
) : IAirportRepository
{
    private IDatabaseService<Airport> DatabaseService { get; } = databaseService;

    public void Add(Airport airport) => DatabaseService.Add(airport);

    public IEnumerable<Airport> GetAll() => DatabaseService.GetAll();

    public Airport? GetById(string id) => DatabaseService
        .GetAll()
        .FirstOrDefault(a => a.Id == id);
}