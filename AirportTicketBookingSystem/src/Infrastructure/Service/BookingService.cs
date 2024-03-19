using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class BookingService(
    IBookingRepository repository,
    IFlightService flightService,
    IPassengerService passengerService
) : IBookingService
{
    private IBookingRepository Repository { get; } = repository;

    private IFlightService FlightService { get; } = flightService;

    private IPassengerService PassengerService { get; } = passengerService;

    private void CheckValidRelationsOrThrow(Booking booking)
    {
        if (PassengerService.GetById(booking.PassengerId) == null)
            throw new DatabaseRelationalException(
                $"Passenger with ID '{booking.PassengerId}' was not found for the booking '{booking}'");

        if (FlightService.GetById(booking.FlightId) == null)
            throw new DatabaseRelationalException(
                $"Flight with ID '{booking.FlightId}' was not found for the booking '{booking}'");
    }

    public void Add(Booking booking)
    {
        CheckValidRelationsOrThrow(booking);
        Repository.Add(booking);
    }

    public void Update(Booking booking)
    {
        CheckValidRelationsOrThrow(booking);
        Repository.Update(booking);
    }

    public void Delete(Booking booking) => Repository.Delete(booking);

    public Booking? GetById(int flightId, int passengerId) => Repository.GetById(flightId, passengerId);

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria)
    {
        var query = Repository.GetAll();

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