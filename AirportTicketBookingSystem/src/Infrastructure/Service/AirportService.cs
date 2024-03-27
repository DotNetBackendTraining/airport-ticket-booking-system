using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class AirportService : IAirportService
{
    private readonly IAirportRepository _repository;
    private readonly IFilteringService<Airport, AirportSearchCriteria> _filteringService;

    public AirportService(
        IAirportRepository repository,
        IFilteringService<Airport, AirportSearchCriteria> filteringService)
    {
        _repository = repository;
        _filteringService = filteringService;
    }

    public void Add(Airport airport) => _repository.Add(airport);

    public Airport? GetById(string id) => _repository.GetById(id);

    public IEnumerable<Airport> Search(AirportSearchCriteria criteria) =>
        _filteringService.Filter(_repository.GetAll(), criteria);
}