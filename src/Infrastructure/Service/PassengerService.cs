using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class PassengerService(
    ISimpleDatabaseService<Passenger> databaseService
) : IPassengerService
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