using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Service;

public class ManagerService(
    IBookingRepository bookingRepository,
    IFlightRepository flightRepository,
    IUploadService<Flight> flightUploadService,
    IReflectionService reflectionService
) : IManagerService
{
    private IBookingRepository BookingRepository { get; } = bookingRepository;

    private IFlightRepository FlightRepository { get; } = flightRepository;

    private IUploadService<Flight> FlightUploadService { get; } = flightUploadService;

    private IReflectionService ReflectionService { get; } = reflectionService;

    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria)
    {
        var bookings = BookingRepository.Search(criteria);
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }

    public OperationResult<Flight> AddFlight(Flight flight)
    {
        try
        {
            FlightRepository.Add(flight);
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
        FlightUploadService.BatchUpload(filepath);

    public IEnumerable<Type> GetDomainEntities() =>
        ReflectionService.GetClassTypesInNamespace("AirportTicketBookingSystem.Domain")
            .Where(type => typeof(IEntity).IsAssignableFrom(type));
}