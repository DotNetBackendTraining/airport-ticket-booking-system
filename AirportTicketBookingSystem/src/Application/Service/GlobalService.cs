using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class GlobalService(
    IFlightService flightService,
    IAirportService airportService
) : IGlobalService
{
    private IFlightService FlightService { get; } = flightService;

    private IAirportService AirportService { get; } = airportService;

    public SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria)
    {
        var flights = FlightService.Search(criteria);
        return new SearchResult<Flight>(
            Success: true,
            Message: "Flights search completed successfully",
            Items: flights);
    }

    public SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria)
    {
        var airports = AirportService.Search(criteria);
        return new SearchResult<Airport>(
            Success: true,
            Message: "Airports search completed successfully",
            Items: airports);
    }
}