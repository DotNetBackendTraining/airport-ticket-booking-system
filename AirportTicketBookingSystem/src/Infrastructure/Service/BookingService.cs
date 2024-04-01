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

    public async Task AddAsync(Booking booking) => await _repository.AddAsync(booking);

    public async Task UpdateAsync(Booking booking) => await _repository.UpdateAsync(booking);

    public async Task DeleteAsync(Booking booking) => await _repository.DeleteAsync(booking);

    public Booking? GetById(int flightId, int passengerId) => _repository.GetById(flightId, passengerId);

    public IEnumerable<Booking> Search(BookingSearchCriteria criteria) =>
        _filteringService.Filter(_repository.GetAll(), criteria);
}