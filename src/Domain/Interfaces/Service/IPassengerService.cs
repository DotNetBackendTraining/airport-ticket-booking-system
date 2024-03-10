namespace AirportTicketBookingSystem.Domain.Interfaces.Service;

/// <summary>
/// Defines the interface for passenger repository operations, providing methods for adding and retrieving passengers.
/// </summary>
public interface IPassengerService
{
    /// <summary>
    /// Adds a new passenger to the repository.
    /// </summary>
    /// <param name="passenger">The passenger to add.</param>
    /// <exception cref="ArgumentException">Thrown when a passenger with the same identifier already exists in the repository.</exception>
    public void Add(Passenger passenger);

    /// <summary>
    /// Retrieves a passenger by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the passenger.</param>
    /// <returns>The passenger with the specified identifier, or null if no such passenger exists.</returns>
    public Passenger? GetById(int id);
}