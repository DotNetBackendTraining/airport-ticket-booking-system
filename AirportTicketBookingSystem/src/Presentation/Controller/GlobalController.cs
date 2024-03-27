using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Presentation.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class GlobalController
{
    protected IServiceProvider Provider { get; }
    protected IGlobalService GlobalService => Provider.GetRequiredService<IGlobalService>();

    public GlobalController(IServiceProvider serviceProvider) => Provider = serviceProvider;

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