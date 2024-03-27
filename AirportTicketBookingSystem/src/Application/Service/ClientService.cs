using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class ClientService : IClientService
{
    private readonly IBookingService _bookingService;
    private readonly IPassengerService _passengerService;

    public ClientService(IBookingService bookingService,
        IPassengerService passengerService)
    {
        _bookingService = bookingService;
        _passengerService = passengerService;
    }

    public SearchResult<Booking> GetAllBookings(int passengerId)
    {
        var bookings = _bookingService.Search(new BookingSearchCriteria { PassengerId = passengerId });
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public OperationResult<Booking> AddBooking(Booking booking)
    {
        try
        {
            _bookingService.Add(booking);
            return new OperationResult<Booking>(
                Success: true,
                Message: "Booking creation completed successfully",
                Item: booking);
        }
        catch (DatabaseException e)
        {
            return new OperationResult<Booking>(
                Success: false,
                Message: "Booking creation failed:  " + e.Message,
                Item: booking);
        }
    }

    public OperationResult<Booking> UpdateBooking(Booking updatedBooking)
    {
        try
        {
            _bookingService.Update(updatedBooking);
            return new OperationResult<Booking>(
                Success: true,
                Message: "Booking update completed successfully",
                Item: updatedBooking);
        }
        catch (DatabaseException e)
        {
            return new OperationResult<Booking>(
                Success: false,
                Message: "Booking update failed:  " + e.Message,
                Item: updatedBooking);
        }
    }

    public OperationResult<Booking> CancelBooking(Booking cancelledBooking)
    {
        try
        {
            _bookingService.Delete(cancelledBooking);
            return new OperationResult<Booking>(
                Success: true,
                Message: "Booking delete completed successfully",
                Item: cancelledBooking);
        }
        catch (DatabaseException e)
        {
            return new OperationResult<Booking>(
                Success: false,
                Message: "Booking delete failed:  " + e.Message,
                Item: cancelledBooking);
        }
    }

    public bool IsPassengerRegistered(int passengerId) =>
        _passengerService.GetById(passengerId) != null;
}