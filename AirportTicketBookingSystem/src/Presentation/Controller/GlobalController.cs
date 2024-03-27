using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Presentation.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace AirportTicketBookingSystem.Presentation.Controller;

public class GlobalController(IServiceProvider serviceProvider)
{
    protected IServiceProvider Provider { get; } = serviceProvider;
    protected IGlobalService GlobalService => Provider.GetRequiredService<IGlobalService>();

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