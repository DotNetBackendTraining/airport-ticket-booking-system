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
    private readonly IFilteringService<Booking, BookingSearchCriteria> _filteringService;

    public BookingService(
        IBookingRepository repository,
        IFlightService flightService,
        IPassengerService passengerService,
        IFilteringService<Booking, BookingSearchCriteria> filteringService)
    {
        _repository = repository;
        _flightService = flightService;
        _passengerService = passengerService;
        _filteringService = filteringService;
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

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria) =>
        _filteringService.Filter(_repository.GetAll(), criteria);
}