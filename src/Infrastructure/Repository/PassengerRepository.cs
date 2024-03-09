using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class PassengerRepository : IPassengerRepository
{
    private readonly ISimpleDatabaseService<Passenger> _databaseService;
    public PassengerRepository(ISimpleDatabaseService<Passenger> databaseService)
    {
        _databaseService = databaseService;
    }

    public void Add(Passenger passenger) => _databaseService.Add(passenger);

    public Passenger? GetById(int id)
    {
        return _databaseService
            .GetAll()
            .FirstOrDefault(p => p.Id == id);
    }
}