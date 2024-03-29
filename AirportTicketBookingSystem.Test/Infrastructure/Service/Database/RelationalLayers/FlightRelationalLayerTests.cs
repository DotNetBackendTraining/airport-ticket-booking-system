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

public class FlightRelationalLayerTests
{
    [Theory, AutoMoqData]
    public async Task AddOrUpdate_ShouldThrowIfAirportDoesNotExist(
        Flight flight,
        [Frozen] Mock<IQueryDatabaseService<Airport>> airportQueryServiceMock,
        FlightRelationalLayer relationalLayer)
    {
        airportQueryServiceMock
            .Setup(s => s.GetAll())
            .Returns([]);

        var addAction = () => relationalLayer.Add(flight);
        var updateAction = () => relationalLayer.Update(flight);

        await addAction.Should().ThrowAsync<DatabaseRelationalException>();
        await updateAction.Should().ThrowAsync<DatabaseRelationalException>();
    }

    [Theory, AutoMoqData]
    public async Task Delete_ShouldThrowIfRelatedBookingExist(
        [Frozen] int fixedBookingId,
        Booking booking,
        Flight flight,
        [Frozen] Mock<IQueryDatabaseService<Booking>> bookingQueryServiceMock,
        FlightRelationalLayer relationalLayer)
    {
        bookingQueryServiceMock
            .Setup(s => s.GetAll())
            .Returns([booking]);

        var action = () => relationalLayer.Delete(flight);

        await action.Should().ThrowAsync<DatabaseRelationalException>();
    }
}