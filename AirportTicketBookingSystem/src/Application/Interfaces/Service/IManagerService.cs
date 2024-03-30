using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Interfaces.Service;

/// <summary>
/// Defines the operations available to managers.
/// </summary>
public interface IManagerService
{
    SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria);

    OperationResult<Flight> AddFlight(Flight flight);

    IEnumerable<OperationResult<Flight>> BatchUploadFlights(string filepath);

    IEnumerable<Type> GetDomainEntities();

    string ReportConstraints(Type type);
}