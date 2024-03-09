using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly ISimpleDatabaseService<Booking> _databaseService;
    private readonly IFlightRepository _flightRepository;
    private readonly IPassengerRepository _passengerRepository;
    
    public BookingRepository(
        ISimpleDatabaseService<Booking> databaseService,
        IFlightRepository flightRepository,
        IPassengerRepository passengerRepository)
    {
        _databaseService = databaseService;
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
    }

    public void Add(Booking booking)
    {
        if (_passengerRepository.GetById(booking.PassengerId) == null)
            throw new InvalidOperationException(
                $"Passenger with ID '{booking.PassengerId}' was not found for the booking '{booking}'");

        if (_flightRepository.GetById(booking.FlightId) == null)
            throw new InvalidOperationException(
                $"Flight with ID '{booking.FlightId}' was not found for the booking '{booking}'");

        _databaseService.Add(booking);
    }

    public void Update(Booking booking) => _databaseService.Update(booking);

    public void Delete(Booking booking) => _databaseService.Delete(booking);

    public Booking? GetById(int flightId, int passengerId)
    {
        return _databaseService
            .GetAll()
            .FirstOrDefault(b =>
                b.FlightId == flightId &&
                b.PassengerId == passengerId);
    }

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria)
    {
        var query = _databaseService.GetAll();

        if (criteria.PassengerId.HasValue)
            query = query.Where(b => b.PassengerId == criteria.PassengerId.Value);

        if (criteria.Flight != null)
            query = query.Where(b => MatchingFlight(b.FlightId, criteria.Flight));

        return query;
    }

    private bool MatchingFlight(int flightId, FlightSearchCriteria flightCriteria)
    {
        var flight = _flightRepository.GetById(flightId);
        return flight != null && 
               _flightRepository.Filter(new List<Flight>{flight}, flightCriteria).Any();
    }
}