using AirportTicketBookingSystem.Domain.Common;

namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

public interface IPassengerRepository
{
    /// <exception cref="DatabaseOperationException">Thrown when a passenger with the same identifier already exists in the repository.</exception>
    public Task AddAsync(Passenger passenger);

    public Passenger? GetById(int id);
}