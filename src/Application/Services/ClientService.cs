using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Services;

public class ClientService : IClientService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPassengerRepository _passengerRepository;
    
    public ClientService(
        IBookingRepository bookingRepository,
        IPassengerRepository passengerRepository
    )
    {
        _bookingRepository = bookingRepository;
        _passengerRepository = passengerRepository;
    }

    public SearchResult<Booking> GetAllBookings(int passengerId)
    {
        var bookings = _bookingRepository.Search(new BookingSearchCriteria { PassengerId = passengerId });
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public OperationResult<Booking> AddBooking(Booking booking)
    {
        try
        {
            _bookingRepository.Add(booking);
            return new OperationResult<Booking>(
                Success: true,
                Message: "Booking creation completed successfully",
                Item: booking);
        }
        catch (SystemException e)
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
            _bookingRepository.Update(updatedBooking);
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

    public OperationResult<Booking> CancelBooking(Booking cancelledBooking)
    {
        try
        {
            _bookingRepository.Delete(cancelledBooking);
            return new OperationResult<Booking>(
                Success: true,
                Message: "Booking delete completed successfully",
                Item: cancelledBooking);
        }
        catch (KeyNotFoundException e)
        {
            return new OperationResult<Booking>(
                Success: false,
                Message: "Booking delete failed:  " + e.Message,
                Item: cancelledBooking);
        }
    }

    public bool IsRegisteredPassenger(int passengerId) =>
        _passengerRepository.GetById(passengerId) != null;
}