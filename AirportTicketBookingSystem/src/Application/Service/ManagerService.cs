using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class ManagerService : IManagerService
{
    private readonly IBookingService _bookingService;
    private readonly IFlightService _flightService;
    private readonly IUploadService<Flight> _flightUploadService;
    private readonly IReflectionService _reflectionService;

    public ManagerService(IBookingService bookingService,
        IFlightService flightService,
        IUploadService<Flight> flightUploadService,
        IReflectionService reflectionService)
    {
        _bookingService = bookingService;
        _flightService = flightService;
        _flightUploadService = flightUploadService;
        _reflectionService = reflectionService;
    }

    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria)
    {
        var bookings = _bookingService.Search(criteria);
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public OperationResult<Flight> AddFlight(Flight flight)
    {
        try
        {
            _flightService.Add(flight);
            return new OperationResult<Flight>(
                Success: true,
                Message: "Flight creation completed successfully",
                Item: flight);
        }
        catch (DatabaseException e)
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