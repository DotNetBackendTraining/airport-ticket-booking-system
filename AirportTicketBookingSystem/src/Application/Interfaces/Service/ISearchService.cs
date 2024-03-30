using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;

namespace AirportTicketBookingSystem.Application.Interfaces.Service;

public interface ISearchService
{
    SearchResult<Flight> SearchFlights(FlightSearchCriteria criteria);

    SearchResult<Airport> SearchAirports(AirportSearchCriteria criteria);

    SearchResult<Booking> SearchBookings(BookingSearchCriteria criteria);
}