using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Repository;

public interface IBookingRepository
{
    public void Add(Booking booking);

    public void Update(Booking booking);

    public void Delete(int flightId, int passengerId);

    public Booking GetById(int flightId, int passengerId);

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria);
}