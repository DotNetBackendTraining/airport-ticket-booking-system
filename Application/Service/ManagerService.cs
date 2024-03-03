using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;
using AirportTicketBookingSystem.Domain.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class ManagerService(
    IBookingRepository bookingRepository
) : IManagerService
{
    private IBookingRepository BookingRepository { get; } = bookingRepository;

    public IEnumerable<Booking> SearchBookings(BookingSearchCriteria criteria)
    {
        return BookingRepository.Search(criteria);
    }
}