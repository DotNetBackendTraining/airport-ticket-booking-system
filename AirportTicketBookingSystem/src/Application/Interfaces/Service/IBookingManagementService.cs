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

    OperationResult<Booking> AddBooking(Booking booking);

    OperationResult<Booking> UpdateBooking(Booking updatedBooking);

    OperationResult<Booking> CancelBooking(Booking cancelledBooking);
}