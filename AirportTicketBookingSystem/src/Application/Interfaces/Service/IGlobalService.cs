using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Interfaces.Service;

/// <summary>
/// Defines the operations available to anyone.
/// </summary>
public interface IGlobalService
{
    SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria);

    SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria);
}