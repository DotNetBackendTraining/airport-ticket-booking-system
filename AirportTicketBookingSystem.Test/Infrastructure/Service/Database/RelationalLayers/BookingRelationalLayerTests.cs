using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Infrastructure.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Service.Database.RelationalLayers;
using AirportTicketBookingSystem.Test.Common;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service.Database.RelationalLayers;

public class BookingRelationalLayerTests
{
    [Theory, AutoMoqData]
    public async Task AddOrUpdate_ShouldThrowIfFlightDoesNotExist(
        Booking booking,
        [Frozen] Mock<IQueryDatabaseService<Flight>> flightQueryServiceMock,
        BookingRelationalLayer relationalLayer)
    {
        flightQueryServiceMock
            .Setup(s => s.GetAll())
            .Returns([]);

        var addAction = () => relationalLayer.Add(booking);
        var updateAction = () => relationalLayer.Update(booking);

        await addAction.Should().ThrowAsync<DatabaseRelationalException>();
        await updateAction.Should().ThrowAsync<DatabaseRelationalException>();
    }

    [Theory, AutoMoqData]
    public async Task AddOrUpdate_ShouldThrowIfPassengerDoesNotExist(
        Booking booking,
        [Frozen] Mock<IQueryDatabaseService<Passenger>> passengerQueryServiceMock,
        BookingRelationalLayer relationalLayer)
    {
        passengerQueryServiceMock
            .Setup(s => s.GetAll())
            .Returns([]);

        var addAction = () => relationalLayer.Add(booking);
        var updateAction = () => relationalLayer.Update(booking);

        await addAction.Should().ThrowAsync<DatabaseRelationalException>();
        await updateAction.Should().ThrowAsync<DatabaseRelationalException>();
    }
}