using AirportTicketBookingSystem.Domain.Common;

namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

public interface IAirportRepository
{
    /// <exception cref="DatabaseOperationException">Thrown when an airport with the same identifier already exists in the repository.</exception>
    public Task AddAsync(Airport airport);

    public IEnumerable<Airport> GetAll();

    public Airport? GetById(string id);
}