using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Repository;

public interface IFlightRepository
{
    public void Add(Flight flight);

    public Task AddAllAsync(IEnumerable<Flight> flights);

    public Flight? GetById(int flightId);

    public IEnumerable<Flight> Search(FlightSearchCriteria criteria);

    public IEnumerable<Flight> Filter(IEnumerable<Flight> flights, FlightSearchCriteria criteria);
}