using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Contract;

/// <summary>
/// Defines the service operations available to managers, including functionality for searching bookings.
/// </summary>
public interface IManagerService
{
    // can we have an interface for search in general, and create new 3 interfaces one for each type of returned entities?
    // IBookingSearchService, IFlightSearchService, IAirportSearchService
    // this way we can have a more specific interface for each type of search
    // and we can have a more specific implementation for each type of search
    // and we can have a security layer if the user is not allowed to search for a specific type of entity
    
    /// <summary>
    /// Searches for bookings that match the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria used to filter bookings.</param>
    /// <returns>A SearchResult object containing a collection of bookings that match the criteria, along with the search operation's success status and message.</returns>
    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria);

    
    //can we have an interface for flight in general, and create new service for it
    //IFlightService
    //this way we can have a more specific interface for each type of operation
    //and we can use it in any service can have access to it
    // same comment for BatchUploadFlights it should be in flight service
    
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

    /// <summary>
    /// Retrieves a collection of all domain entities available in the system.
    /// </summary>
    /// <returns>
    /// An enumerable of <see cref="Type"/> objects representing each domain class in the system.
    /// </returns>
    public IEnumerable<Type> GetDomainEntities();

    /// <summary>
    /// Generates a report listing properties and their attributes for a given type.
    /// </summary>
    /// <param name="type">The type to report on.</param>
    /// <returns>A formatted string report detailing each property of the specified type and its associated attributes.</returns>
    public string ReportConstraints(Type type);
}