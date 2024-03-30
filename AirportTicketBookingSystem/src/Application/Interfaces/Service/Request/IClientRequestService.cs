using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Interfaces.Service.Request;

/// <summary>
/// Defines the operations available to clients.
/// </summary>
public interface IClientRequestService
{
    SearchResult<Booking> GetAllBookings(int passengerId);

    OperationResult<Booking> AddBooking(Booking booking);

    OperationResult<Booking> UpdateBooking(Booking updatedBooking);

    OperationResult<Booking> CancelBooking(Booking cancelledBooking);

    bool IsPassengerRegistered(int passengerId);
}