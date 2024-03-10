using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class BookingService(
    ISimpleDatabaseService<Booking> databaseService,
    IFlightService flightService,
    IPassengerService passengerService
) : IBookingService
{
    private ISimpleDatabaseService<Booking> DatabaseService { get; } = databaseService;

    private IFlightService FlightService { get; } = flightService;

    private IPassengerService PassengerService { get; } = passengerService;

    public void Add(Booking booking)
    {
        if (PassengerService.GetById(booking.PassengerId) == null)
            throw new InvalidOperationException(
                $"Passenger with ID '{booking.PassengerId}' was not found for the booking '{booking}'");

        if (FlightService.GetById(booking.FlightId) == null)
            throw new InvalidOperationException(
                $"Flight with ID '{booking.FlightId}' was not found for the booking '{booking}'");

        DatabaseService.Add(booking);
    }

    public void Update(Booking booking) => DatabaseService.Update(booking);

    public void Delete(Booking booking) => DatabaseService.Delete(booking);

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
        var flight = FlightService.GetById(flightId);
        return FlightService.Filter([flight], flightCriteria).Any();
    }
}