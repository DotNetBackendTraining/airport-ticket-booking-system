using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;
    private readonly IFilteringService<Flight, FlightSearchCriteria> _filteringService;

    public FlightService(
        IFlightRepository repository,
        IFilteringService<Flight, FlightSearchCriteria> filteringService)
    {
        _repository = repository;
        _filteringService = filteringService;
    }

    public void Add(Flight flight) => _repository.Add(flight);

    public Flight? GetById(int flightId) => _repository.GetById(flightId);

    public IEnumerable<Flight> Search(FlightSearchCriteria criteria) =>
        _filteringService.Filter(_repository.GetAll(), criteria);
}