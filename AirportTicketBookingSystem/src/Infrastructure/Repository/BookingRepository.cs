using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly IDatabaseService<Booking> _databaseService;
    public BookingRepository(IDatabaseService<Booking> databaseService) => _databaseService = databaseService;

    public void Add(Booking booking) => _databaseService.Add(booking);

    public void Update(Booking booking) => _databaseService.Update(booking);

    public void Delete(Booking booking) => _databaseService.Delete(booking);

    public IEnumerable<Booking> GetAll() => _databaseService.GetAll();

    public Booking? GetById(int flightId, int passengerId) => _databaseService
        .GetAll()
        .FirstOrDefault(b =>
            b.FlightId == flightId &&
            b.PassengerId == passengerId);
}