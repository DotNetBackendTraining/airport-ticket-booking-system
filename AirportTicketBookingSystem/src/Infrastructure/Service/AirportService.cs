using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class AirportService(
    IAirportRepository repository
) : IAirportService
{
    private IAirportRepository Repository { get; } = repository;

    public void Add(Airport airport) => Repository.Add(airport);

    public Airport? GetById(string id) => Repository.GetById(id);

    public IEnumerable<Airport> Search(AirportSearchCriteria criteria)
    {
        return Filter(Repository.GetAll(), criteria);
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