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

public class AirportRelationalLayerTests
{
    [Theory, AutoMoqData]
    public async Task AddOrUpdate_ShouldNeverThrow(
        Airport airport,
        AirportRelationalLayer relationalLayer)
    {
        var addAction = () => relationalLayer.Add(airport);
        var updateAction = () => relationalLayer.Update(airport);

        await addAction.Should().NotThrowAsync();
        await updateAction.Should().NotThrowAsync();
    }

    [Theory, AutoMoqData]
    public async Task Delete_ShouldThrowIfRelatedFlightExist(
        [Frozen] string fixedAirportId,
        Flight flight,
        Airport airport,
        [Frozen] Mock<IQueryDatabaseService<Flight>> flightQueryServiceMock,
        AirportRelationalLayer relationalLayer)
    {
        flightQueryServiceMock
            .Setup(s => s.GetAll())
            .Returns([flight]);

        var action = () => relationalLayer.Delete(airport);

        await action.Should().ThrowAsync<DatabaseRelationalException>();
    }
}