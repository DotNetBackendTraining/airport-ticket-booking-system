using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class AirportService : IAirportService
{
    private readonly IAirportRepository _repository;
    public AirportService(IAirportRepository repository) => _repository = repository;

    public void Add(Airport airport) => _repository.Add(airport);

    public Airport? GetById(string id) => _repository.GetById(id);

    public IEnumerable<Airport> Search(AirportSearchCriteria criteria)
    {
        return Filter(_repository.GetAll(), criteria);
    }

    public IEnumerable<Airport> Filter(IEnumerable<Airport> airports, AirportSearchCriteria criteria)
    {
        if (!string.IsNullOrEmpty(criteria.Name))
            airports = airports.Where(a => a.Name == criteria.Name);

        if (!string.IsNullOrEmpty(criteria.Country))
            airports = airports.Where(a => a.Country == criteria.Country);

        return airports;
    }
}