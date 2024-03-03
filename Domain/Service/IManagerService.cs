using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Service;

/// <summary>
/// Defines the service operations available to managers
/// </summary>
public interface IManagerService
{
    /// <summary>
    /// Searches for bookings that match the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria used to filter bookings.</param>
    /// <returns>An enumerable collection of bookings that match the criteria.</returns>
    public IEnumerable<Booking> SearchBookings(BookingSearchCriteria criteria);
}