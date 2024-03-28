using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;

public class PassengerRelationalLayer : DatabaseRelationalLayer<Passenger>
{
    private readonly IQueryDatabaseService<Booking> _bookingQueryService;

    public PassengerRelationalLayer(
        ICrudDatabaseService<Passenger> databaseService,
        IQueryDatabaseService<Booking> bookingQueryService)
        : base(databaseService)
    {
        _bookingQueryService = bookingQueryService;
    }

    protected override void ValidateAddOrThrow(Passenger entity)
    {
    }

    protected override void ValidateUpdateOrThrow(Passenger newEntity)
    {
    }

    protected override void ValidateDeleteOrThrow(Passenger entity)
    {
        if (_bookingQueryService.GetAll().Any(b => b.PassengerId == entity.Id))
            throw new DatabaseRelationalException(
                $"Must first delete all bookings for passenger with ID '{entity.Id}'");
    }
}