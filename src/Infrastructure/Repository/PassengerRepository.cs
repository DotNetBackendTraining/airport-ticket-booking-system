using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class PassengerRepository(
    ISimpleDatabaseService<Passenger> databaseService
) : IPassengerRepository
{
    private ISimpleDatabaseService<Passenger> DatabaseService { get; } = databaseService;

    public void Add(Passenger passenger) => DatabaseService.Add(passenger);

    public Passenger? GetById(int id) => DatabaseService
        .GetAll()
        .FirstOrDefault(p => p.Id == id);
}