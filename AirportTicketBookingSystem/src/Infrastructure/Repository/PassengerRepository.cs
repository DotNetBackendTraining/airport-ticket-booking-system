using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class PassengerRepository : IPassengerRepository
{
    private readonly IDatabaseService<Passenger> _databaseService;
    public PassengerRepository(IDatabaseService<Passenger> databaseService) => _databaseService = databaseService;

    public void Add(Passenger passenger) => _databaseService.Add(passenger);

    public Passenger? GetById(int id) => _databaseService
        .GetAll()
        .FirstOrDefault(p => p.Id == id);
}