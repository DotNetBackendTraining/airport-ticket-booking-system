using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

/// <summary>
/// Defines the interface for booking repository operations, providing methods for adding, updating, deleting,
/// and retrieving bookings, as well as searching for bookings based on criteria.
/// </summary>
public interface IBookingRepository
{
    /// <summary>
    /// Adds a new booking to the repository.
    /// </summary>
    /// <param name="booking">The booking to add.</param>
    /// <exception cref="ArgumentException">Thrown when an identical booking already exists in the repository.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the <c>flightId</c> or <c>passengerId</c> in the booking do not exist in the repository.</exception>
    public void Add(Booking booking);

    /// <summary>
    /// Updates an existing booking in the repository.
    /// </summary>
    /// <param name="booking">The booking with updated information.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the booking to update does not exist in the repository.</exception>
    public void Update(Booking booking);

    /// <summary>
    /// Deletes a booking from the repository based on flight and passenger identifiers.
    /// </summary>
    /// <param name="booking">The booking to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the booking to delete does not exist in the repository.</exception>
    public void Delete(Booking booking);

    /// <summary>
    /// Retrieves a booking by flight and passenger identifiers.
    /// </summary>
    /// <param name="flightId">The identifier of the flight associated with the booking.</param>
    /// <param name="passengerId">The identifier of the passenger associated with the booking.</param>
    /// <returns>The booking if found; otherwise, <c>null</c>.</returns>
    public Booking? GetById(int flightId, int passengerId);

    /// <summary>
    /// Searches for bookings matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria to apply to the search.</param>
    /// <returns>A collection of bookings that match the search criteria.</returns>
    public IEnumerable<Booking> Search(BookingSearchCriteria criteria);
}