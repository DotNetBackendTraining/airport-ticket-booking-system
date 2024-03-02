using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Repository;

public interface IBookingRepository
{
    public void Add(Booking booking);

    public bool Update(Booking booking);

    public bool Delete(int flightId, int passengerId);

    public Booking? GetById(int flightId, int passengerId);

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria);
}