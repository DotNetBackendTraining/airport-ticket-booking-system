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
    private IBookingService BookingService { get; }
    private IFlightService FlightService { get; }
    private IUploadService<Flight> FlightUploadService { get; }
    private IReflectionService ReflectionService { get; }

    public ManagerService(IBookingService bookingService,
        IFlightService flightService,
        IUploadService<Flight> flightUploadService,
        IReflectionService reflectionService)
    {
        BookingService = bookingService;
        FlightService = flightService;
        FlightUploadService = flightUploadService;
        ReflectionService = reflectionService;
    }

    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria)
    {
        var bookings = BookingService.Search(criteria);
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public OperationResult<Flight> AddFlight(Flight flight)
    {
        try
        {
            FlightService.Add(flight);
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
        FlightUploadService.BatchUpload(filepath);

    public IEnumerable<Type> GetDomainEntities() =>
        ReflectionService.GetClassTypesInNamespace("AirportTicketBookingSystem.Domain")
            .Where(type => typeof(IEntity).IsAssignableFrom(type));

    public string ReportConstraints(Type type) =>
        ReflectionService.ReportPropertiesWithAttributes(type);
}