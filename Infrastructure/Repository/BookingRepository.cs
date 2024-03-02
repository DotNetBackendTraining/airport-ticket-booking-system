using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class BookingRepository(
    IFileService<Booking> fileService,
    IFlightRepository flightRepository
) : IBookingRepository
{
    private IFileService<Booking> FileService { get; } = fileService;

    private IFlightRepository FlightRepository { get; } = flightRepository;

    public void Add(Booking booking)
    {
        FileService.Append(booking);
    }

    public bool Update(Booking booking)
    {
        var old = GetById(booking.FlightId, booking.PassengerId);
        return old != null && FileService.Replace(old, booking);
    }

    public bool Delete(int flightId, int passengerId)
    {
        var booking = GetById(flightId, passengerId);
        return booking != null && FileService.Remove(booking);
    }

    public Booking? GetById(int flightId, int passengerId)
    {
        return FileService
            .ReadAll()
            .FirstOrDefault(b =>
                b.FlightId == flightId &&
                b.PassengerId == passengerId);
    }

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria)
    {
        var query = FileService.ReadAll();

        if (criteria.PassengerId.HasValue)
            query = query.Where(b => b.PassengerId == criteria.PassengerId.Value);

        if (criteria.Flight != null)
            query = query.Where(b => MatchingFlight(b.FlightId, criteria.Flight));

        return query;
    }

    private bool MatchingFlight(int flightId, FlightSearchCriteria flightCriteria)
    {
        var flight = FlightRepository.GetById(flightId);
        return FlightRepository.Filter([flight], flightCriteria).Any();
    }
}