using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Service;

/// <summary>
/// Defines the service operations available to anyone
/// </summary>
public interface IGlobalService
{
    /// <summary>
    /// Searches for flights that match the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria used to filter flights.</param>
    /// <returns>An enumerable collection of flights that match the criteria.</returns>
    public IEnumerable<Flight> SearchFlights(FlightSearchCriteria criteria);
}