using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;

public class BookingRelationalLayer : DatabaseRelationalLayer<Booking>
{
    private readonly IQueryDatabaseService<Flight> _flightQueryService;
    private readonly IQueryDatabaseService<Passenger> _passengerQueryService;

    public BookingRelationalLayer(
        ICrudDatabaseService<Booking> databaseService,
        IQueryDatabaseService<Flight> flightQueryService,
        IQueryDatabaseService<Passenger> passengerQueryService)
        : base(databaseService)
    {
        _flightQueryService = flightQueryService;
        _passengerQueryService = passengerQueryService;
    }

    protected override void ValidateAddOrThrow(Booking entity)
    {
        ValidateFlightExistsOrThrow(entity);
        ValidatePassengerExistsOrThrow(entity);
    }

    protected override void ValidateUpdateOrThrow(Booking newEntity)
    {
        ValidateFlightExistsOrThrow(newEntity);
        ValidatePassengerExistsOrThrow(newEntity);
    }

    protected override void ValidateDeleteOrThrow(Booking entity)
    {
    }

    private void ValidateFlightExistsOrThrow(Booking entity)
    {
        if (_flightQueryService.GetAll().All(f => f.Id != entity.FlightId))
            throw new DatabaseRelationalException($"Flight with ID '{entity.FlightId}' does not exist");
    }

    private void ValidatePassengerExistsOrThrow(Booking entity)
    {
        if (_passengerQueryService.GetAll().All(f => f.Id != entity.PassengerId))
            throw new DatabaseRelationalException($"Passenger with ID '{entity.PassengerId}' does not exist");
    }
}