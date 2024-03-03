using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;
using AirportTicketBookingSystem.Domain.Service;

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
        return BookingRepository.Update(updatedBooking);
    }

    public bool CancelBooking(int flightId, int passengerId)
    {
        return BookingRepository.Delete(flightId, passengerId);
    }
}