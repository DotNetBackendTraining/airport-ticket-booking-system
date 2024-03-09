using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Services;

public class ManagerService : IManagerService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IFlightRepository _flightRepository;
    private readonly IUploadService<Flight> _flightUploadService;
    private readonly IReflectionService _reflectionService;
    
    public ManagerService(
        IBookingRepository bookingRepository,
        IFlightRepository flightRepository,
        IUploadService<Flight> flightUploadService,
        IReflectionService reflectionService)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _flightUploadService = flightUploadService;
        _reflectionService = reflectionService;
    }

    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria)
    {
        var bookings = _bookingRepository.Search(criteria);
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public OperationResult<Flight> AddFlight(Flight flight)
    {
        try
        {
            _flightRepository.Add(flight);
            return new OperationResult<Flight>(
                Success: true,
                Message: "Flight creation completed successfully",
                Item: flight);
        }
        catch (SystemException e)
        {
            return new OperationResult<Flight>(
                Success: false,
                Message: "Flight creation failed:  " + e.Message,
                Item: flight);
        }
    }

    public IEnumerable<OperationResult<Flight>> BatchUploadFlights(string filepath) =>
        _flightUploadService.BatchUpload(filepath);

    public IEnumerable<Type> GetDomainEntities() =>
        _reflectionService.GetClassTypesInNamespace("AirportTicketBookingSystem.Domain")
            .Where(type => typeof(IEntity).IsAssignableFrom(type));

    public string ReportConstraints(Type type) =>
        _reflectionService.ReportPropertiesWithAttributes(type);
}