using AirportTicketBookingSystem.Domain.Common;

namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

public interface IBookingRepository
{
    /// <exception cref="DatabaseOperationException">Thrown when an identical booking already exists in the repository.</exception>
    public void Add(Booking booking);

    /// <exception cref="DatabaseOperationException">Thrown when the booking to update does not exist in the repository.</exception>
    public void Update(Booking booking);

    /// <exception cref="DatabaseOperationException">Thrown when the booking to delete does not exist in the repository.</exception>
    public void Delete(Booking booking);

    public IEnumerable<Booking> GetAll();

    public Booking? GetById(int flightId, int passengerId);
}