using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository : IAirportRepository
{
    private readonly ISimpleDatabaseService<Airport> _databaseService;
    public AirportRepository(ISimpleDatabaseService<Airport> databaseService)
    {
        _databaseService = databaseService;
    }

    public void Add(Airport airport) => _databaseService.Add(airport);

    public Airport? GetById(string id)
    {
        return _databaseService
            .GetAll()
            .FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Airport> Search(AirportSearchCriteria criteria)
    {
        return Filter(_databaseService.GetAll(), criteria);
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