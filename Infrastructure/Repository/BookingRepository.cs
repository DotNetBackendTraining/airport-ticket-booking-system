using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class BookingRepository(
    ISimpleDatabaseService<Booking> databaseService,
    IFlightRepository flightRepository
) : IBookingRepository
{
    private ISimpleDatabaseService<Booking> DatabaseService { get; } = databaseService;

    private IFlightRepository FlightRepository { get; } = flightRepository;

    public void Add(Booking booking)
    {
        DatabaseService.Add(booking);
    }

    public bool Update(Booking booking)
    {
        DatabaseService.Update(booking);
        return true;
    }

    public bool Delete(int flightId, int passengerId)
    {
        var booking = GetById(flightId, passengerId);
        return booking != null && DatabaseService.Delete(booking);
    }

    public Booking? GetById(int flightId, int passengerId)
    {
        return DatabaseService
            .GetAll()
            .FirstOrDefault(b =>
                b.FlightId == flightId &&
                b.PassengerId == passengerId);
    }

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria)
    {
        var query = DatabaseService.GetAll();

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