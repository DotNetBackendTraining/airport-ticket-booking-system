using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Interfaces.Service.Request;

/// <summary>
/// Defines the operations available to clients.
/// </summary>
public interface IClientRequestService
{
    SearchResult<Booking> GetAllBookings(int passengerId);

    Task<OperationResult<Booking>> AddBookingAsync(Booking booking);

    Task<OperationResult<Booking>> UpdateBookingAsync(Booking updatedBooking);

    Task<OperationResult<Booking>> CancelBookingAsync(Booking cancelledBooking);

    bool IsPassengerRegistered(int passengerId);
}