using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class BookingRepository(
    ISimpleDatabaseService<Booking> databaseService,
    IFlightRepository flightRepository,
    IPassengerRepository passengerRepository
) : IBookingRepository
{
    private ISimpleDatabaseService<Booking> DatabaseService { get; } = databaseService;

    private IFlightRepository FlightRepository { get; } = flightRepository;

    private IPassengerRepository PassengerRepository { get; } = passengerRepository;

    public void Add(Booking booking)
    {
        if (PassengerRepository.GetById(booking.PassengerId) == null)
            throw new InvalidOperationException(
                $"Passenger with ID '{booking.PassengerId}' was not found for the booking '{booking}'");

        if (FlightRepository.GetById(booking.FlightId) == null)
            throw new InvalidOperationException(
                $"Flight with ID '{booking.FlightId}' was not found for the booking '{booking}'");

        DatabaseService.Add(booking);
    }

    public void Update(Booking booking) => DatabaseService.Update(booking);

    public bool Delete(Booking booking) => DatabaseService.Delete(booking);

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