using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class FlightRepository : IFlightRepository
{
    private IDatabaseService<Flight> DatabaseService { get; }
    public FlightRepository(IDatabaseService<Flight> databaseService) => DatabaseService = databaseService;

    public void Add(Flight flight) => DatabaseService.Add(flight);

    public IEnumerable<Flight> GetAll() => DatabaseService.GetAll();

    public Flight? GetById(int flightId) => DatabaseService
        .GetAll()
        .FirstOrDefault(f => f.Id == flightId);
}