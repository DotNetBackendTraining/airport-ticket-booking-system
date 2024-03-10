using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository(
    ISimpleDatabaseService<Airport> databaseService
) : IAirportRepository
{
    private ISimpleDatabaseService<Airport> DatabaseService { get; } = databaseService;

    public void Add(Airport airport) => DatabaseService.Add(airport);

    public IEnumerable<Airport> GetAll() => DatabaseService.GetAll();

    public Airport? GetById(string id) => DatabaseService
        .GetAll()
        .FirstOrDefault(a => a.Id == id);
}