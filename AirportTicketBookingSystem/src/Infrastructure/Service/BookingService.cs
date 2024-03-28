using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;
    private readonly IFilteringService<Booking, BookingSearchCriteria> _filteringService;

    public BookingService(
        IBookingRepository repository,
        IFilteringService<Booking, BookingSearchCriteria> filteringService)
    {
        _repository = repository;
        _filteringService = filteringService;
    }

    public void Add(Booking booking) => _repository.Add(booking);

    public void Update(Booking booking) => _repository.Update(booking);

    public void Delete(Booking booking) => _repository.Delete(booking);

    public Booking? GetById(int flightId, int passengerId) => _repository.GetById(flightId, passengerId);

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria) =>
        _filteringService.Filter(_repository.GetAll(), criteria);
}