using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Service;

public class ClientService : IClientService
{
    private readonly IBookingManagementService _bookingManagementService;
    private readonly IPassengerRegistrationService _passengerRegistrationService;

    public ClientService(
        IBookingManagementService bookingManagementService,
        IPassengerRegistrationService passengerRegistrationService)
    {
        _bookingManagementService = bookingManagementService;
        _passengerRegistrationService = passengerRegistrationService;
    }

    public SearchResult<Booking> GetAllBookings(int passengerId) =>
        _bookingManagementService.GetAllBookings(passengerId);

    public OperationResult<Booking> AddBooking(Booking booking) =>
        _bookingManagementService.AddBooking(booking);

    public OperationResult<Booking> UpdateBooking(Booking updatedBooking) =>
        _bookingManagementService.UpdateBooking(updatedBooking);

    public OperationResult<Booking> CancelBooking(Booking cancelledBooking) =>
        _bookingManagementService.CancelBooking(cancelledBooking);

    public bool IsPassengerRegistered(int passengerId) =>
        _passengerRegistrationService.IsPassengerRegistered(passengerId);
}