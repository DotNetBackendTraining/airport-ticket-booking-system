using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class BookingRepository(
    IDatabaseService<Booking> databaseService
) : IBookingRepository
{
    private IDatabaseService<Booking> DatabaseService { get; } = databaseService;

    public void Add(Booking booking) => DatabaseService.Add(booking);

    public void Update(Booking booking) => DatabaseService.Update(booking);

    public void Delete(Booking booking) => DatabaseService.Delete(booking);

    public IEnumerable<Booking> GetAll() => DatabaseService.GetAll();

    public Booking? GetById(int flightId, int passengerId) => DatabaseService
        .GetAll()
        .FirstOrDefault(b =>
            b.FlightId == flightId &&
            b.PassengerId == passengerId);
}