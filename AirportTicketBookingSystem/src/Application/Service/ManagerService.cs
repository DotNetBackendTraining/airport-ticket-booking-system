using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Service;

public class ManagerService : IManagerService
{
    private readonly ISearchService _searchService;
    private readonly IFlightManagementService _flightManagementService;
    private readonly IUploadService<Flight> _flightUploadService;
    private readonly IReflectionService _reflectionService;


    public ManagerService(
        ISearchService searchService,
        IFlightManagementService flightManagementService,
        IUploadService<Flight> flightUploadService,
        IReflectionService reflectionService)
    {
        _searchService = searchService;
        _flightManagementService = flightManagementService;
        _flightUploadService = flightUploadService;
        _reflectionService = reflectionService;
    }

    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria) =>
        _searchService.SearchBookings(criteria);

    public OperationResult<Flight> AddFlight(Flight flight) =>
        _flightManagementService.AddFlight(flight);

    public IEnumerable<OperationResult<Flight>> BatchUploadFlights(string filepath) =>
        _flightUploadService.BatchUpload(filepath);

    public IEnumerable<Type> GetDomainEntities() =>
        _reflectionService.GetDomainEntityTypes();

    public string ReportConstraints(Type type) =>
        _reflectionService.ReportPropertiesWithAttributes(type);
}