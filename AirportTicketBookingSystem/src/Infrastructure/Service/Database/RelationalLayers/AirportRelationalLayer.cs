using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;

public class AirportRelationalLayer : DatabaseRelationalLayer<Airport>
{
    public AirportRelationalLayer(ICrudDatabaseService<Airport> databaseService) : base(databaseService)
    {
    }

    protected override void ValidateAddOrThrow(Airport entity)
    {
    }

    protected override void ValidateUpdateOrThrow(Airport newEntity)
    {
    }

    protected override void ValidateDeleteOrThrow(Airport entity)
    {
    }
}