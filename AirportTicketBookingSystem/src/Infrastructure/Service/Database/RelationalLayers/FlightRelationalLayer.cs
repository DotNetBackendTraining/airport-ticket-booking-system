using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;

public class FlightRelationalLayer : DatabaseRelationalLayer<Flight>
{
    private readonly IQueryDatabaseService<Airport> _airportQueryService;
    private readonly IQueryDatabaseService<Booking> _bookingQueryService;

    public FlightRelationalLayer(
        ICrudDatabaseService<Flight> databaseService,
        IQueryDatabaseService<Airport> airportQueryService,
        IQueryDatabaseService<Booking> bookingQueryService)
        : base(databaseService)
    {
        _airportQueryService = airportQueryService;
        _bookingQueryService = bookingQueryService;
    }

    protected override void ValidateAddOrThrow(Flight entity)
    {
        ValidateBothAirportsExistOrThrow(entity);
    }

    protected override void ValidateUpdateOrThrow(Flight newEntity)
    {
        ValidateBothAirportsExistOrThrow(newEntity);
    }

    protected override void ValidateDeleteOrThrow(Flight entity)
    {
        if (_bookingQueryService.GetAll().Any(b => b.FlightId == entity.Id))
            throw new DatabaseRelationalException($"Must first delete all bookings for flight with ID '{entity.Id}'");
    }

    private void ValidateBothAirportsExistOrThrow(Flight entity)
    {
        foreach (var id in new[] { entity.DepartureAirportId, entity.ArrivalAirportId })
            if (_airportQueryService.GetAll().All(a => a.Id != id))
                throw new DatabaseRelationalException($"Airport with ID '{id}' does not exist");
    }
}