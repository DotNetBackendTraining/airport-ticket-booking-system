using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly IQueryDatabaseService<Booking> _queryDatabaseService;
    private readonly ICrudDatabaseService<Booking> _crudDatabaseService;

    public BookingRepository(
        IQueryDatabaseService<Booking> queryDatabaseService,
        ICrudDatabaseService<Booking> crudDatabaseService)
    {
        _queryDatabaseService = queryDatabaseService;
        _crudDatabaseService = crudDatabaseService;
    }

    public void Add(Booking booking) => _crudDatabaseService.Add(booking);

    public void Update(Booking booking) => _crudDatabaseService.Update(booking);

    public void Delete(Booking booking) => _crudDatabaseService.Delete(booking);

    public IEnumerable<Booking> GetAll() => _queryDatabaseService.GetAll();

    public Booking? GetById(int flightId, int passengerId) => _queryDatabaseService
        .GetAll()
        .FirstOrDefault(b =>
            b.FlightId == flightId &&
            b.PassengerId == passengerId);
}