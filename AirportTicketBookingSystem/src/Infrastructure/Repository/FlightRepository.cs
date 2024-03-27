using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class FlightRepository : IFlightRepository
{
    private readonly IDatabaseService<Flight> _databaseService;
    public FlightRepository(IDatabaseService<Flight> databaseService) => _databaseService = databaseService;

    public void Add(Flight flight) => _databaseService.Add(flight);

    public IEnumerable<Flight> GetAll() => _databaseService.GetAll();

    public Flight? GetById(int flightId) => _databaseService
        .GetAll()
        .FirstOrDefault(f => f.Id == flightId);
}