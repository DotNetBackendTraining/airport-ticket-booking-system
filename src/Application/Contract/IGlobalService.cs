using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Contract;

/// <summary>
/// Defines the service operations available to anyone, including flight search functionalities.
/// </summary>
public interface IGlobalService
{
    /// <summary>
    /// Searches for flights that match the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria used to filter flights.</param>
    /// <returns>A SearchResult object containing a collection of flights that match the criteria.</returns>
    public SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria);

    /// <summary>
    /// Searches for airports that match the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria used to filter airports.</param>
    /// <returns>A SearchResult object containing a collection of airports that match the criteria.</returns>
    public SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria);
}