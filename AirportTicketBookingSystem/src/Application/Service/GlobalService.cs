using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class GlobalService : IGlobalService
{
    private readonly IFlightService _flightService;
    private readonly IAirportService _airportService;

    public GlobalService(IFlightService flightService,
        IAirportService airportService)
    {
        _flightService = flightService;
        _airportService = airportService;
    }

    public SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria)
    {
        var flights = _flightService.Search(criteria);
        return new SearchResult<Flight>(
            Success: true,
            Message: "Flights search completed successfully",
            Items: flights);
    }

    public SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria)
    {
        var airports = _airportService.Search(criteria);
        return new SearchResult<Airport>(
            Success: true,
            Message: "Airports search completed successfully",
            Items: airports);
    }
}