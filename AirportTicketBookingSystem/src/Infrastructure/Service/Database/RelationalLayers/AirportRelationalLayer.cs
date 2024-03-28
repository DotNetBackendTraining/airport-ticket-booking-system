using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;

public class AirportRelationalLayer : DatabaseRelationalLayer<Airport>
{
    private readonly IQueryDatabaseService<Flight> _flightQueryService;

    public AirportRelationalLayer(
        ICrudDatabaseService<Airport> databaseService,
        IQueryDatabaseService<Flight> flightQueryService)
        : base(databaseService)
    {
        _flightQueryService = flightQueryService;
    }

    protected override void ValidateAddOrThrow(Airport entity)
    {
    }

    protected override void ValidateUpdateOrThrow(Airport newEntity)
    {
    }

    protected override void ValidateDeleteOrThrow(Airport entity)
    {
        var id = entity.Id;
        if (_flightQueryService.GetAll().Any(b => b.ArrivalAirportId == id || b.DepartureAirportId == id))
            throw new DatabaseRelationalException($"Must first delete all flights that include airport with ID '{id}'");
    }
}