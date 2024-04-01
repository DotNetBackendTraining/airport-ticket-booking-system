using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Interfaces.Service;

/// <summary>
/// Defines the interface for airport repository operations, providing methods for adding, retrieving,
/// searching, and filtering airports.
/// </summary>
public interface IAirportService
{
    /// <summary>
    /// Adds a new airport to the repository.
    /// </summary>
    /// <param name="airport">The airport to add.</param>
    /// <exception cref="DatabaseOperationException">Thrown when an airport with the same identifier already exists in the repository.</exception>
    Task AddAsync(Airport airport);

    /// <summary>
    /// Retrieves an airport by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the airport.</param>
    /// <returns>The airport with the specified identifier, or null if no such airport exists.</returns>
    Airport? GetById(string id);

    /// <summary>
    /// Searches for airports matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria to apply to the search.</param>
    /// <returns>A collection of airports that match the search criteria.</returns>
    IEnumerable<Airport> Search(AirportSearchCriteria criteria);
}