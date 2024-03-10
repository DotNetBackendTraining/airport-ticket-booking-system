namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

public interface IFlightRepository
{
    /// <exception cref="ArgumentException">Thrown when a flight with the same identifier already exists in the repository.</exception>
    public void Add(Flight flight);

    public IEnumerable<Flight> GetAll();

    public Flight? GetById(int flightId);
}