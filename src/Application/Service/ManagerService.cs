using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Service;

public class ManagerService(
    IBookingRepository bookingRepository
) : IManagerService
{
    private IBookingRepository BookingRepository { get; } = bookingRepository;

    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria)
    {
        var bookings = BookingRepository.Search(criteria);
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }
}