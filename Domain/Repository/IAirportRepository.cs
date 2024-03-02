using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Domain.Repository;

public interface IAirportRepository
{
    void Add(Airport airport);

    public Airport? GetById(string id);

    public IEnumerable<Airport> Search(AirportSearchCriteria criteria);
}