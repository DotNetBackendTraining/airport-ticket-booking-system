using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Service;

public class ClientService(
    IBookingRepository bookingRepository
) : IClientService
{
    private IBookingRepository BookingRepository { get; } = bookingRepository;

    public SearchResult<Booking> GetAllBookings(int passengerId)
    {
        var bookings = BookingRepository.Search(new BookingSearchCriteria { PassengerId = passengerId });
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public OperationResult<Booking> UpdateBooking(Booking updatedBooking)
    {
        try
        {
            BookingRepository.Update(updatedBooking);
            return new OperationResult<Booking>(
                Success: true,
                Message: "Booking update completed successfully",
                Item: updatedBooking);
        }
        catch (KeyNotFoundException e)
        {
            return new OperationResult<Booking>(
                Success: false,
                Message: "Booking update failed:  " + e.Message,
                Item: updatedBooking);
        }
    }

    public OperationResult<Booking> CancelBooking(int flightId, int passengerId)
    {
        var booking = BookingRepository.GetById(flightId, passengerId);
        if (booking == null)
            return new OperationResult<Booking>(
                Success: false,
                Message: "Booking cancellation failed:  Booking not found in the system",
                Item: null);

        BookingRepository.Delete(booking);
        return new OperationResult<Booking>(
            Success: true,
            Message: "Booking update completed successfully",
            Item: booking);
    }
}