using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class AirportRepository(
    IFileService<Airport> fileService
) : IAirportRepository
{
    private IFileService<Airport> FileService { get; } = fileService;

    public void Add(Airport airport)
    {
        FileService.Append(airport);
    }

    public Airport? GetById(string id)
    {
        return FileService
            .ReadAll()
            .FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Airport> Search(AirportSearchCriteria criteria)
    {
        return Filter(FileService.ReadAll(), criteria);
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