using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class BookingManagementService : IBookingManagementService
{
    private readonly IBookingService _bookingService;
    public BookingManagementService(IBookingService bookingService) => _bookingService = bookingService;

    public SearchResult<Booking> GetAllBookings(int passengerId)
    {
        var bookings = _bookingService.Search(new BookingSearchCriteria { PassengerId = passengerId });
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public async Task<OperationResult<Booking>> AddBookingAsync(Booking booking)
    {
        try
        {
            await _bookingService.AddAsync(booking);
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

    public async Task<OperationResult<Booking>> UpdateBookingAsync(Booking updatedBooking)
    {
        try
        {
            await _bookingService.UpdateAsync(updatedBooking);
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

    public async Task<OperationResult<Booking>> CancelBookingAsync(Booking cancelledBooking)
    {
        try
        {
            await _bookingService.DeleteAsync(cancelledBooking);
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
}