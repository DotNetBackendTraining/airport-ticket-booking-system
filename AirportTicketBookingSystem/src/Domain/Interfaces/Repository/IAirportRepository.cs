namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

public interface IAirportRepository
{
    /// <exception cref="ArgumentException">Thrown when an airport with the same identifier already exists in the repository.</exception>
    public void Add(Airport airport);

    public IEnumerable<Airport> GetAll();

    public Airport? GetById(string id);
}