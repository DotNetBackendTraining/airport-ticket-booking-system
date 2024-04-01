using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Interfaces.Service;

public interface IBookingManagementService
{
    /// <summary>
    /// Retrieves all bookings for a given passenger.
    /// </summary>
    /// <param name="passengerId">The ID of the passenger whose bookings to retrieve.</param>
    /// <returns>A SearchResult containing a collection of bookings for the specified passenger.</returns>
    SearchResult<Booking> GetAllBookings(int passengerId);

    Task<OperationResult<Booking>> AddBookingAsync(Booking booking);

    Task<OperationResult<Booking>> UpdateBookingAsync(Booking updatedBooking);

    Task<OperationResult<Booking>> CancelBookingAsync(Booking cancelledBooking);
}