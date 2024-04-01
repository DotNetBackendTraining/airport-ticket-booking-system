using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Interfaces.Service.Request;

/// <summary>
/// Defines the operations available to managers.
/// </summary>
public interface IManagerRequestService
{
    SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria);

    Task<OperationResult<Flight>> AddFlightAsync(Flight flight);

    IEnumerable<OperationResult<Flight>> BatchUploadFlights(string filepath);

    IEnumerable<Type> GetDomainEntities();

    string ReportConstraints(Type type);
}