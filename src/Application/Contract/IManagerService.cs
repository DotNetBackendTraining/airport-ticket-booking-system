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
}