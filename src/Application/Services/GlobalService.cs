using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Services;

public class GlobalService : IGlobalService
{
    // split this to use two services
    // one for flight and one for airport
    // and avoid calling the repository directly from general service used for many things
    // this will make the code more maintainable and easier to understand
    // and also avoid the need to change the global service when the repository changes
    // private readonly IFlightService _flightService;
    // private readonly IAirportService _airportService;
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