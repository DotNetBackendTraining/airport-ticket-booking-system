using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;
    private readonly IAirportService _airportService;
    private readonly IFilteringService<Flight, FlightSearchCriteria> _filteringService;

    public FlightService(
        IFlightRepository repository,
        IAirportService airportService,
        IFilteringService<Flight, FlightSearchCriteria> filteringService)
    {
        _repository = repository;
        _airportService = airportService;
        _filteringService = filteringService;
    }

    public void Add(Flight flight)
    {
        foreach (var id in new[] { flight.DepartureAirportId, flight.ArrivalAirportId })
            if (_airportService.GetById(id) == null)
                throw new DatabaseRelationalException($"Airport with ID '{id}' was not found in the repository");
        _repository.Add(flight);
    }

    public Flight? GetById(int flightId) => _repository.GetById(flightId);

    public IEnumerable<Flight> Search(FlightSearchCriteria criteria) =>
        _filteringService.Filter(_repository.GetAll(), criteria);
}