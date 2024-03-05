using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Contract;

/// <summary>
/// Defines the service operations available to managers, including functionality for searching bookings.
/// </summary>
public interface IManagerService
{
    /// <summary>
    /// Searches for bookings that match the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria used to filter bookings.</param>
    /// <returns>A SearchResult object containing a collection of bookings that match the criteria, along with the search operation's success status and message.</returns>
    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria);

    /// <summary>
    /// Adds a new flight to the system.
    /// </summary>
    /// <param name="flight">The flight to add.</param>
    /// <returns>An OperationResult corresponding to the addition operation of the flight.</returns>
    public OperationResult<Flight> AddFlight(Flight flight);

    /// <summary>
    /// Uploads a batch of <see cref="Flight"/> entities from a specified file.
    /// </summary>
    /// <param name="filepath">The path to the file containing flight data to be uploaded.</param>
    /// <returns>
    /// An enumerable of <see cref="OperationResult{Flight}"/>, representing the result of each flight upload operation.
    /// Each result includes the flight entity, a success flag, and an optional error message if the upload failed.
    /// </returns>
    /// <exception cref="FileNotFoundException">Thrown if the file specified by <paramref name="filepath"/> does not exist.</exception>
    public IEnumerable<OperationResult<Flight>> BatchUploadFlights(string filepath);
}