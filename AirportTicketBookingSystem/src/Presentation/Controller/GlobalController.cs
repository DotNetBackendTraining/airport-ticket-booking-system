using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Presentation.Utility;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class GlobalController
{
    protected readonly IGlobalService GlobalService;
    public GlobalController(IGlobalService globalService) => GlobalService = globalService;

    protected void SearchFlights()
    {
        var flightCriteria = PromptFilter.PromptFlightFilter();
        var flights = GlobalService.SearchFlights(flightCriteria);
        Display.SearchResult(flights);
    }

    protected void SearchAirports()
    {
        var criteria = PromptCriteria.AirportCriteria() ?? new AirportSearchCriteria();
        var airports = GlobalService.SearchAirports(criteria);
        Display.SearchResult(airports);
    }
}