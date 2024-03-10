using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository(
    ISimpleDatabaseService<Airport> databaseService
) : IAirportRepository
{
    private ISimpleDatabaseService<Airport> DatabaseService { get; } = databaseService;

    public void Add(Airport airport) => DatabaseService.Add(airport);

    public Airport? GetById(string id)
    {
        return DatabaseService
            .GetAll()
            .FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Airport> Search(AirportSearchCriteria criteria)
    {
        return Filter(DatabaseService.GetAll(), criteria);
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