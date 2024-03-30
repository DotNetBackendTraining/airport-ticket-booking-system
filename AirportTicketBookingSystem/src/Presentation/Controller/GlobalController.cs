using AirportTicketBookingSystem.Application.Interfaces.Service.Request;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Presentation.Utility;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class GlobalController
{
    protected readonly IGlobalRequestService GlobalRequestService;
    public GlobalController(IGlobalRequestService globalRequestService) => GlobalRequestService = globalRequestService;

    protected void SearchFlights()
    {
        var flightCriteria = PromptFilter.PromptFlightFilter();
        var flights = GlobalRequestService.SearchFlights(flightCriteria);
        Display.SearchResult(flights);
    }

    protected void SearchAirports()
    {
        var criteria = PromptCriteria.AirportCriteria() ?? new AirportSearchCriteria();
        var airports = GlobalRequestService.SearchAirports(criteria);
        Display.SearchResult(airports);
    }
}