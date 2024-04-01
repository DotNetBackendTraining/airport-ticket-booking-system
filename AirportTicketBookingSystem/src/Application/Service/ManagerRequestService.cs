using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Interfaces.Service.Request;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Service;

public class ManagerRequestService : IManagerRequestService
{
    private readonly ISearchService _searchService;
    private readonly IFlightManagementService _flightManagementService;
    private readonly IUploadService<Flight> _flightUploadService;
    private readonly IReflectionService _reflectionService;


    public ManagerRequestService(
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

    public async Task<OperationResult<Flight>> AddFlightAsync(Flight flight) =>
        await _flightManagementService.AddFlightAsync(flight);

    public IEnumerable<OperationResult<Flight>> BatchUploadFlights(string filepath) =>
        _flightUploadService.BatchUpload(filepath);

    public IEnumerable<Type> GetDomainEntities() =>
        _reflectionService.GetDomainEntityTypes();

    public string ReportConstraints(Type type) =>
        _reflectionService.ReportPropertiesWithAttributes(type);
}