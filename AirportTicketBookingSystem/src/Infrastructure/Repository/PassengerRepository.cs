using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class PassengerRepository : IPassengerRepository
{
    private IDatabaseService<Passenger> DatabaseService { get; }
    public PassengerRepository(IDatabaseService<Passenger> databaseService) => DatabaseService = databaseService;

    public void Add(Passenger passenger) => DatabaseService.Add(passenger);

    public Passenger? GetById(int id) => DatabaseService
        .GetAll()
        .FirstOrDefault(p => p.Id == id);
}