using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Application.Service;

public class GlobalService(
    IFlightRepository flightRepository
) : IGlobalService
{
    private IFlightRepository FlightRepository { get; } = flightRepository;

    public SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria)
    {
        var flights = FlightRepository.Search(criteria);
        return new SearchResult<Flight>(
            Success: true,
            Message: "Flights search completed successfully",
            Items: flights);
    }
}