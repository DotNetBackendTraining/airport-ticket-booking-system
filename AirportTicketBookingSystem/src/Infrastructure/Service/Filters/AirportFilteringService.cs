using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service.Filters;

public class AirportFilteringService : IFilteringService<Airport, AirportSearchCriteria>
{
    public IEnumerable<Airport> Filter(IEnumerable<Airport> entities, AirportSearchCriteria criteria)
    {
        if (!string.IsNullOrEmpty(criteria.Name))
            entities = entities.Where(a => a.Name == criteria.Name);

        if (!string.IsNullOrEmpty(criteria.Country))
            entities = entities.Where(a => a.Country == criteria.Country);

        return entities;
    }
}