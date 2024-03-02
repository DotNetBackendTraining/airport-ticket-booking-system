using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Repository;

public interface IFlightRepository
{
    public void Add(Flight flight);

    public Flight GetById(int flightId);

    public IEnumerable<Flight> Search(FlightSearchCriteria criteria);
}