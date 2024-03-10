using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;

namespace AirportTicketBookingSystem.Application.Service;

public class GlobalService(
    IFlightRepository flightRepository,
    IAirportRepository airportRepository
) : IGlobalService
{
    private IFlightRepository FlightRepository { get; } = flightRepository;

    private IAirportRepository AirportRepository { get; } = airportRepository;

    public SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria)
    {
        var flights = FlightRepository.Search(criteria);
        return new SearchResult<Flight>(
            Success: true,
            Message: "Flights search completed successfully",
            Items: flights);
    }

    public SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria)
    {
        var airports = AirportRepository.Search(criteria);
        return new SearchResult<Airport>(
            Success: true,
            Message: "Airports search completed successfully",
            Items: airports);
    }
}