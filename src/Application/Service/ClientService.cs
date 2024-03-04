using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Service;

public class ClientService(
    IBookingRepository bookingRepository
) : IClientService
{
    private IBookingRepository BookingRepository { get; } = bookingRepository;

    public IEnumerable<Booking> GetAllBookings(int passengerId)
    {
        return BookingRepository.Search(new BookingSearchCriteria { PassengerId = passengerId });
    }

    public bool UpdateBooking(Booking updatedBooking)
    {
        throw new NotImplementedException();
    }

    public bool CancelBooking(int flightId, int passengerId)
    {
        throw new NotImplementedException();
    }
}