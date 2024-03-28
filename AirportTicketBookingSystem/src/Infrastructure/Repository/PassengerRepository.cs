using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class PassengerRepository : IPassengerRepository
{
    private readonly IQueryDatabaseService<Passenger> _queryDatabaseService;
    private readonly ICrudDatabaseService<Passenger> _crudDatabaseService;

    public PassengerRepository(
        IQueryDatabaseService<Passenger> queryDatabaseService,
        ICrudDatabaseService<Passenger> crudDatabaseService)
    {
        _queryDatabaseService = queryDatabaseService;
        _crudDatabaseService = crudDatabaseService;
    }

    public void Add(Passenger passenger) => _crudDatabaseService.Add(passenger);

    public Passenger? GetById(int id) => _queryDatabaseService
        .GetAll()
        .FirstOrDefault(p => p.Id == id);
}