using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Interfaces.Service.Request;

/// <summary>
/// Defines the operations available to anyone.
/// </summary>
public interface IGlobalRequestService
{
    SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria);

    SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria);
}