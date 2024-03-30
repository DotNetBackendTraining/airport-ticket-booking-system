using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class SearchService : ISearchService
{
    private readonly IFlightService _flightService;
    private readonly IAirportService _airportService;
    private readonly IBookingService _bookingService;

    public SearchService(
        IFlightService flightService,
        IAirportService airportService,
        IBookingService bookingService)
    {
        _flightService = flightService;
        _airportService = airportService;
        _bookingService = bookingService;
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

    public SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria)
    {
        var bookings = _bookingService.Search(criteria);
        return new SearchResult<Booking>(
            Success: true,
            Message: "Bookings search completed successfully",
            Items: bookings);
    }
}