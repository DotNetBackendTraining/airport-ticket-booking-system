using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Services;

public class GlobalService : IGlobalService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAirportRepository _airportRepository;
    
    public GlobalService(IFlightRepository flightRepository, IAirportRepository airportRepository)
    {
        _flightRepository = flightRepository;
        _airportRepository = airportRepository;
    }

    public SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria)
    {
        var flights = _flightRepository.Search(criteria);
        return new SearchResult<Flight>(
            Success: true,
            Message: "Flights search completed successfully",
            Items: flights);
    }

    public SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria)
    {
        var airports = _airportRepository.Search(criteria);
        return new SearchResult<Airport>(
            Success: true,
            Message: "Airports search completed successfully",
            Items: airports);
    }
}