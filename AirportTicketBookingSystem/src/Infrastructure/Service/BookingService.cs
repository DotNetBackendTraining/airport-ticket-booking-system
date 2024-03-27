using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;
    private readonly IFlightService _flightService;
    private readonly IPassengerService _passengerService;

    public BookingService(
        IBookingRepository repository,
        IFlightService flightService,
        IPassengerService passengerService)
    {
        _repository = repository;
        _flightService = flightService;
        _passengerService = passengerService;
    }

    private void CheckValidRelationsOrThrow(Booking booking)
    {
        if (_passengerService.GetById(booking.PassengerId) == null)
            throw new DatabaseRelationalException(
                $"Passenger with ID '{booking.PassengerId}' was not found for the booking '{booking}'");

        if (_flightService.GetById(booking.FlightId) == null)
            throw new DatabaseRelationalException(
                $"Flight with ID '{booking.FlightId}' was not found for the booking '{booking}'");
    }

    public void Add(Booking booking)
    {
        CheckValidRelationsOrThrow(booking);
        _repository.Add(booking);
    }

    public void Update(Booking booking)
    {
        CheckValidRelationsOrThrow(booking);
        _repository.Update(booking);
    }

    public void Delete(Booking booking) => _repository.Delete(booking);

    public Booking? GetById(int flightId, int passengerId) => _repository.GetById(flightId, passengerId);

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria)
    {
        var query = _repository.GetAll();

        if (criteria.PassengerId.HasValue)
            query = query.Where(b => b.PassengerId == criteria.PassengerId.Value);

        if (criteria.Flight != null)
            query = query.Where(b => MatchingFlight(b.FlightId, criteria.Flight));

        return query;
    }

    private bool MatchingFlight(int flightId, FlightSearchCriteria flightCriteria)
    {
        var flight = _flightService.GetById(flightId);
        return _flightService.Filter([flight], flightCriteria).Any();
    }
}