using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class PassengerRepository(
    ISimpleDatabaseService<Passenger> databaseService
) : IPassengerRepository
{
    private ISimpleDatabaseService<Passenger> DatabaseService { get; } = databaseService;

    public void Add(Passenger passenger) => DatabaseService.Add(passenger);

    public Passenger? GetById(int id)
    {
        return DatabaseService
            .GetAll()
            .FirstOrDefault(p => p.Id == id);
    }
}