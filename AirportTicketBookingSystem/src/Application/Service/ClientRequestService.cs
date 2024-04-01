using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Interfaces.Service.Request;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Service;

public class ClientRequestService : IClientRequestService
{
    private readonly IBookingManagementService _bookingManagementService;
    private readonly IPassengerRegistrationService _passengerRegistrationService;

    public ClientRequestService(
        IBookingManagementService bookingManagementService,
        IPassengerRegistrationService passengerRegistrationService)
    {
        _bookingManagementService = bookingManagementService;
        _passengerRegistrationService = passengerRegistrationService;
    }

    public SearchResult<Booking> GetAllBookings(int passengerId) =>
        _bookingManagementService.GetAllBookings(passengerId);

    public async Task<OperationResult<Booking>> AddBookingAsync(Booking booking) =>
        await _bookingManagementService.AddBookingAsync(booking);

    public async Task<OperationResult<Booking>> UpdateBookingAsync(Booking updatedBooking) =>
        await _bookingManagementService.UpdateBookingAsync(updatedBooking);

    public async Task<OperationResult<Booking>> CancelBookingAsync(Booking cancelledBooking) =>
        await _bookingManagementService.CancelBookingAsync(cancelledBooking);

    public bool IsPassengerRegistered(int passengerId) =>
        _passengerRegistrationService.IsPassengerRegistered(passengerId);
}