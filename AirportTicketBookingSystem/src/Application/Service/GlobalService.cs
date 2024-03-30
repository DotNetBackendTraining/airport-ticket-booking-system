using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Service;

public class GlobalService : IGlobalService
{
    private readonly ISearchService _searchService;
    public GlobalService(ISearchService searchService) => _searchService = searchService;

    public SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria) =>
        _searchService.SearchFlights(criteria);

    public SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria) =>
        _searchService.SearchAirports(criteria);
}