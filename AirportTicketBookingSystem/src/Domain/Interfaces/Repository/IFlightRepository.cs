using AirportTicketBookingSystem.Domain.Common;

namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

public interface IFlightRepository
{
    /// <exception cref="DatabaseOperationException">Thrown when a flight with the same identifier already exists in the repository.</exception>
    public void Add(Flight flight);

    public IEnumerable<Flight> GetAll();

    public Flight? GetById(int flightId);
}